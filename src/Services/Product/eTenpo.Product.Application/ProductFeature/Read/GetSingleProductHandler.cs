using eTenpo.Product.Domain.Contracts;
using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Read;

public class GetSingleProductHandler : IRequestHandler<GetSingleProductRequest, GetSingleProductResponse>
{
    private readonly IProductRepository repo;

    public GetSingleProductHandler(IProductRepository repo)
    {
        this.repo = repo;
    }
    
    public async Task<GetSingleProductResponse> Handle(GetSingleProductRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}