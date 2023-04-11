using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Update;

public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, ProductUpdateResponse>
{
    public ProductUpdateCommandHandler()
    {
        
    }
    
    public Task<ProductUpdateResponse> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}