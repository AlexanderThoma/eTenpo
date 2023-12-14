using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace eTenpo.Product.Application.CategoryFeature.Delete;

public class DeleteCategoryCommandHandler(
    ICategoryRepository repository,
    IUnitOfWork unitOfWork,
    ILogger<DeleteCategoryCommandHandler> logger)
    : ICommandHandler<DeleteCategoryCommand>
{
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Try to get the category with id {Id} from database", request.Id);
        
        var category = await repository.FindByIdWithProducts(request.Id, cancellationToken);

        if (category is null)
        {
            throw new EntityNotFoundException($"Category with id {request.Id} could not be found");
        }
        
        logger.LogInformation("Create the productDeleted event");
        category.Delete();
        
        logger.LogInformation("Mark the category as deleted");
        repository.Delete(category);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}