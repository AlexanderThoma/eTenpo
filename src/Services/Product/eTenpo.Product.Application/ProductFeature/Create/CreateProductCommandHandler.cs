using AutoMapper;
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
    private readonly IMapper mapper;

    public CreateProductCommandHandler(IProductRepository repository, ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork, ILogger<CreateProductCommandHandler> logger, IMapper mapper)
    {
        this.repository = repository;
        this.categoryRepository = categoryRepository;
        this.unitOfWork = unitOfWork;
        this.logger = logger;
        this.mapper = mapper;
    }

    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await this.ValidateCategoryId(request.CategoryId);
        await this.ValidateNameUniqueness(request.Name, cancellationToken);
        
        this.logger.LogInformation("Create a new product");
        
        // create in repo, createdEvent called in ctor of aggregate
        var product = new Domain.AggregateRoots.ProductAggregate.Product(
             request.Name,
             request.Price,
             request.Description,
             request.CategoryId);

        await this.repository.Add(product);
        
        this.logger.LogInformation("Add the new product to the database");
        
        await this.unitOfWork.SaveChangesAsync(cancellationToken);
        
        return this.mapper.Map<CreateProductCommandResponse>(product);
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
    
    private async Task ValidateNameUniqueness(string newName, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Validate product name uniqueness");
        
        var productByName = await this.repository.FindByName(newName, cancellationToken);

        if (productByName is not null)
        {
            throw new ProductValidationException("Name already in use",
                new ArgumentException(null, nameof(newName)));
        }
    }
}