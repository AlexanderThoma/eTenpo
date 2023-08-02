using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.AggregateRoots.ProductAggregate;
using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace eTenpo.Product.Application.ProductFeature.Update;

public class ProductUpdateCommandHandler : ICommandHandler<UpdateProductCommand, UpdateProductResponse>
{
    private readonly IProductRepository productRepository;
    private readonly ICategoryRepository categoryRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger<ProductUpdateCommandHandler> logger;

    public ProductUpdateCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, ILogger<ProductUpdateCommandHandler> logger)
    {
        this.productRepository = productRepository;
        this.categoryRepository = categoryRepository;
        this.unitOfWork = unitOfWork;
        this.logger = logger;
    }
    
    public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        await ValidateCategoryId(request.CategoryId);

        var product = await GetProductFromDatabase(request, cancellationToken);

        await ValidateNameUniqueness(request.Name, cancellationToken, product);

        product.UpdateName(new Name(request.Name));
        product.UpdatePrice(new Price(request.Price));
        product.ChangeCategory(new CategoryId(request.CategoryId));
        product.UpdateDescription(new Description(request.Description));

        await this.unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new UpdateProductResponse(product.Id);
    }

    private async Task ValidateNameUniqueness(string newProductName, CancellationToken cancellationToken,
        Domain.AggregateRoots.ProductAggregate.Product product)
    {
        this.logger.LogInformation("Validate product name uniqueness");
        
        var productByName = await this.productRepository.FindByName(newProductName, cancellationToken);

        if (productByName is not null && productByName.Id != product.Id)
        {
            throw new ProductValidationException("Name already in use",
                new ArgumentException(null, nameof(newProductName)));
        }
    }

    private async Task<Domain.AggregateRoots.ProductAggregate.Product> GetProductFromDatabase(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Get product with id {Id} from database", request.Id);
        var product = await this.productRepository.FindById(request.Id, cancellationToken);

        if (product is null)
        {
            throw new EntityNotFoundException($"Product with id {request.Id} could not be found");
        }

        return product;
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