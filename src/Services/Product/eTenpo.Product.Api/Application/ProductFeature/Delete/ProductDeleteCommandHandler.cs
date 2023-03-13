using eTenpo.Product.Api.Application.ProductFeature.Delete;
using MediatR;

namespace eTenpo.Product.Api.Application.ProductFeature.Commands.Delete;

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