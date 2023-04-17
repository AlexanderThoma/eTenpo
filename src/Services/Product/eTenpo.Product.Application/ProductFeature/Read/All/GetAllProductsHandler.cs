using AutoMapper;
using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.Contracts;
using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Read.All;

public class GetAllProductsHandler : IQueryHandler<GetAllProductsRequest, List<GetAllProductsResponse>>
{
    private readonly IProductRepository repo;
    private readonly IMapper mapper;

    public GetAllProductsHandler(IProductRepository repo, IMapper mapper)
    {
        this.repo = repo;
        this.mapper = mapper;
    }
    
    public async Task<List<GetAllProductsResponse>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
    {
        var products = await this.repo.GetAllWithCategory(cancellationToken);

        return this.mapper.Map<List<GetAllProductsResponse>>(products);
    }
}