using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.AggregateRoots.ProductAggregate;
using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Domain.Exceptions;

namespace eTenpo.Product.Application.ProductFeature.Update;

public class ProductUpdateCommandHandler : ICommandHandler<UpdateProductCommand, UpdateProductResponse>
{
    private readonly IProductRepository productRepository;
    private readonly ICategoryRepository categoryRepository;
    private readonly IUnitOfWork unitOfWork;

    public ProductUpdateCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        this.productRepository = productRepository;
        this.categoryRepository = categoryRepository;
        this.unitOfWork = unitOfWork;
    }
    
    public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        if (!await this.categoryRepository.Exists(request.CategoryId))
        {
            throw new ProductValidationException($"Category with Id \"{request.CategoryId}\" does not exist",
                new ArgumentException(null, nameof(request.CategoryId)));
        }
        
        var product = await this.productRepository.GetById(request.Id, cancellationToken);
        
        // name uniqueness check
        var productByName = await this.productRepository.GetByName(request.Name, cancellationToken);

        if (productByName is not null && productByName.Id != product.Id)
        {
            throw new ProductValidationException("Name already in use",
                new ArgumentException(null, nameof(request.Name)));
        }
        
        product.UpdateName(new Name(request.Name));
        product.UpdatePrice(new Price(request.Price));
        product.ChangeCategory(new CategoryId(request.CategoryId));
        product.UpdateDescription(new Description(request.Description));

        await this.unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new UpdateProductResponse(product.Id);
    }
}