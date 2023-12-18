using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.AggregateRoots.CategoryAggregate;
using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace eTenpo.Product.Application.CategoryFeature.Update;

public class UpdateCategoryCommandHandler(
    ICategoryRepository repository,
    IUnitOfWork unitOfWork,
    ILogger<UpdateCategoryCommandHandler> logger)
    : ICommandHandler<UpdateCategoryCommand, UpdateCategoryCommandResponse>
{
    public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = await GetCategoryFromDatabase(request, cancellationToken);

        await ValidateNameUniqueness(request.Name, cancellationToken, category);

        category.UpdateName(new CategoryName(request.Name));
        category.UpdateDescription(new CategoryDescription(request.Description));
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new UpdateCategoryCommandResponse(category.Id, category.Name.Value, category.Description.Value);
    }

    private async Task<Category> GetCategoryFromDatabase(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get category with id {Id} from database", request.Id);
        var category = await repository.FindById(request.Id, cancellationToken);

        if (category is null)
        {
            throw new EntityNotFoundException($"Category with id {request.Id} could not be found");
        }

        await ValidateNameUniqueness(request.Name, cancellationToken, category);
        
        return category;
    }

    private async Task ValidateNameUniqueness(string newCategoryName,
        CancellationToken cancellationToken,
        Category category)
    {
        logger.LogInformation("Validate product name uniqueness");

        var categoryName = await repository.FindByName(newCategoryName, cancellationToken);

        if (categoryName is not null && categoryName.Id != category.Id)
        {
            throw new CategoryValidationException("Name already in use",
                new ArgumentException(null, nameof(newCategoryName)));
        }
    }
}