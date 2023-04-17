using AutoMapper;
using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.Contracts;

namespace eTenpo.Product.Application.ProductFeature.Read.Single;

public class GetSingleProductHandler : IQueryHandler<GetSingleProductRequest, GetSingleProductResponse>
{
    private readonly IProductRepository repo;
    private readonly IMapper mapper;

    public GetSingleProductHandler(IProductRepository repo, IMapper mapper)
    {
        this.repo = repo;
        this.mapper = mapper;
    }
    
    public async Task<GetSingleProductResponse> Handle(GetSingleProductRequest request, CancellationToken cancellationToken)
    {
        var product = await this.repo.GetByIdWithCategory(request.Id, cancellationToken);

        return this.mapper.Map<GetSingleProductResponse>(product);
    }
}