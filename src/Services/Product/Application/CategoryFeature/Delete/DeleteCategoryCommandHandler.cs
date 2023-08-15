using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace eTenpo.Product.Application.CategoryFeature.Delete;

public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand>
{
    private readonly ICategoryRepository repository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<DeleteCategoryCommandHandler> logger;

    public DeleteCategoryCommandHandler(ICategoryRepository repository, IUnitOfWork unitOfWork, ILogger<DeleteCategoryCommandHandler> logger)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        this.logger = logger;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to get the category with id {Id} from database", request.Id);
        
        var category = await this.repository.FindByIdWithProducts(request.Id, cancellationToken);

        if (category is null)
        {
            throw new EntityNotFoundException($"Category with id {request.Id} could not be found");
        }
        
        this.logger.LogInformation("Create the productDeleted event");
        category.Delete();
        
        this.logger.LogInformation("Mark the category as deleted");
        this.repository.Delete(category);
        
        await this.unitOfWork.SaveChangesAsync(cancellationToken);
    }
}