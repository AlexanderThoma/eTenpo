using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Delete;

public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommand, ProductDeleteResponse>
{
    public ProductDeleteCommandHandler()
    {
        
    }
    
    public Task<ProductDeleteResponse> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}