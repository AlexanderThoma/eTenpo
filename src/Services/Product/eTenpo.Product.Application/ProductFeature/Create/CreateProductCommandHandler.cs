using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace eTenpo.Product.Application.ProductFeature.Create;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductCommandResponse>
{
    private readonly IProductRepository repository;
    private readonly ICategoryRepository categoryRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<CreateProductCommandHandler> logger;

    public CreateProductCommandHandler(IProductRepository repository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, ILogger<CreateProductCommandHandler> logger)
    {
        this.repository = repository;
        this.categoryRepository = categoryRepository;
        this.unitOfWork = unitOfWork;
        this.logger = logger;
    }
    
    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await ValidateCategoryId(request.CategoryId);
        
        this.logger.LogInformation("Create a new product");
        
        // create in repo, createdEvent called in ctor of aggregate
        var product = new Domain.AggregateRoots.ProductAggregate.Product(
             request.Name,
             request.Price,
             request.Description,
             request.CategoryId);

        var id = await this.repository.Add(product);
        
        this.logger.LogInformation("Add the new product to the database");
        
        await this.unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new CreateProductCommandResponse(id);
    }
    
    private async Task ValidateCategoryId(Guid categoryId)
    {
        this.logger.LogInformation("Validate if categoryId exists");

        if (!await this.categoryRepository.Exists(categoryId))
        {
            throw new ProductValidationException($"Category with Id \"{categoryId}\" does not exist",
                new ArgumentException(null, nameof(categoryId)));
        }
    }
}