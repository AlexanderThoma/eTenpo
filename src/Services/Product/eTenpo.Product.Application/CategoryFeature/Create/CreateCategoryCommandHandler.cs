using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.AggregateRoots.CategoryAggregate;
using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace eTenpo.Product.Application.CategoryFeature.Create;

public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
{
    private readonly ICategoryRepository repository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<CreateCategoryCommandHandler> logger;

    public CreateCategoryCommandHandler(ICategoryRepository repository, IUnitOfWork unitOfWork, ILogger<CreateCategoryCommandHandler> logger)
    {
        this.repository = repository;
        this.unitOfWork = unitOfWork;
        this.logger = logger;
    }
    
    public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Create a new category");

        await this.ValidateNameUniqueness(request.Name, cancellationToken);
        
        // create in repo, createdEvent called in ctor of aggregate
        var category = new Category(request.Name, request.Description);

        await this.repository.Add(category);
        
        this.logger.LogInformation("Add the new category to the database");
        
        await this.unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateCategoryCommandResponse(category.Id, category.Name, category.Description);
    }
    
    private async Task ValidateNameUniqueness(string newName, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Validate product name uniqueness");
        
        var productByName = await this.repository.FindByName(newName, cancellationToken);

        if (productByName is not null)
        {
            throw new CategoryValidationException("Name already in use",
                new ArgumentException(null, nameof(newName)));
        }
    }
}