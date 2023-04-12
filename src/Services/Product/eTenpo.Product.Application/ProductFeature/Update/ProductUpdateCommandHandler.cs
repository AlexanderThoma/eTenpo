using eTenpo.Product.Domain.Contracts;
using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Update;

public class ProductUpdateCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
{
    private readonly IProductRepository repo;

    public ProductUpdateCommandHandler(IProductRepository repo)
    {
        this.repo = repo;
    }
    
    public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}