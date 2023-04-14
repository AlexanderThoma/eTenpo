using eTenpo.Product.Domain.Contracts;
using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Read;

public class GetAllProductsRequestHandler : IRequestHandler<GetAllProductsRequest, List<GetAllProductsResponse>>
{
    private readonly IProductRepository repo;

    public GetAllProductsRequestHandler(IProductRepository repo)
    {
        this.repo = repo;
    }
    
    public async Task<List<GetAllProductsResponse>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}