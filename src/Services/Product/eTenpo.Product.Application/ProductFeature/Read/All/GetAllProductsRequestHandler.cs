using AutoMapper;
using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.Contracts;
using Microsoft.Extensions.Logging;

namespace eTenpo.Product.Application.ProductFeature.Read.All;

public class GetAllProductsRequestHandler : IQueryHandler<GetAllProductsRequest, List<GetAllProductsRequestResponse>>
{
    private readonly IProductRepository repo;
    private readonly IMapper mapper;
    private readonly ILogger<GetAllProductsRequestHandler> logger;

    public GetAllProductsRequestHandler(IProductRepository repo, IMapper mapper, ILogger<GetAllProductsRequestHandler> logger)
    {
        this.repo = repo;
        this.mapper = mapper;
        this.logger = logger;
    }
    
    public async Task<List<GetAllProductsRequestResponse>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Get all products with category from database");
        
        var products = await this.repo.GetAllWithCategory(cancellationToken);

        return this.mapper.Map<List<GetAllProductsRequestResponse>>(products);
    }
}