using AutoMapper;
using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Domain.Exceptions;

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
        var product = await this.repo.FindByIdWithCategory(request.Id, cancellationToken);

        if (product is null)
        {
            throw new EntityNotFoundException($"Product with id {request.Id} could not be found");
        }
        
        return this.mapper.Map<GetSingleProductResponse>(product);
    }
}