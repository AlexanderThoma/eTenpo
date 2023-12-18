using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.AggregateRoots.CategoryAggregate;
using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace eTenpo.Product.Application.CategoryFeature.Create;

public class CreateCategoryCommandHandler(
    ICategoryRepository repository,
    IUnitOfWork unitOfWork,
    ILogger<CreateCategoryCommandHandler> logger)
    : ICommandHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
{
    public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Create a new category");

        await ValidateNameUniqueness(request.Name, cancellationToken);
        
        // create in repo, createdEvent called in ctor of aggregate
        var category = new Category(request.Name, request.Description);

        await repository.Add(category);
        
        logger.LogInformation("Add the new category to the database");
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateCategoryCommandResponse(category.Id, category.Name, category.Description);
    }
    
    private async Task ValidateNameUniqueness(string newName, CancellationToken cancellationToken)
    {
        logger.LogInformation("Validate product name uniqueness");
        
        var productByName = await repository.FindByName(newName, cancellationToken);

        if (productByName is not null)
        {
            throw new CategoryValidationException("Name already in use",
                new ArgumentException(null, nameof(newName)));
        }
    }
}