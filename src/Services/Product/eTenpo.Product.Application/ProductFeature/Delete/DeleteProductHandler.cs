using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.Contracts;

namespace eTenpo.Product.Application.ProductFeature.Delete;

public class DeleteProductHandler : ICommandHandler<DeleteProductCommand>
{
    private readonly IProductRepository productRepository;

    public DeleteProductHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }
    
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await this.productRepository.GetById(request.Id, cancellationToken);
        
        // mark as deleted in changeTracker
        this.productRepository.Delete(product);
        
        // generate domain event
        product.Delete();
    }
}