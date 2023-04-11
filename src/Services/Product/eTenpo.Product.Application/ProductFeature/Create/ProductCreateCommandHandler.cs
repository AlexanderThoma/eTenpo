using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Create;

public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, ProductCreateResponse>
{
    public ProductCreateCommandHandler()
    {
        
    }
    
    public Task<ProductCreateResponse> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}