using eTenpo.Product.Domain.Contracts;
using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Delete;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResponse>
{
    private readonly IProductRepository productRepository;

    public DeleteProductHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }
    
    public async Task<DeleteProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await this.productRepository.GetById(request.Id);
        
        // mark as deleted in changeTracker
        this.productRepository.Delete(product);
        
        // generate domain event
        product.Delete();

        return new DeleteProductResponse(request.Id);
    }
}