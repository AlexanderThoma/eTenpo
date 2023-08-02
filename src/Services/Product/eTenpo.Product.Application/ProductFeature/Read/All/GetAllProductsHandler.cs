using AutoMapper;
using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eTenpo.Product.Application.ProductFeature.Read.All;

public class GetAllProductsHandler : IQueryHandler<GetAllProductsRequest, List<GetAllProductsResponse>>
{
    private readonly IProductRepository repo;
    private readonly IMapper mapper;
    private readonly ILogger<GetAllProductsHandler> logger;

    public GetAllProductsHandler(IProductRepository repo, IMapper mapper, ILogger<GetAllProductsHandler> logger)
    {
        this.repo = repo;
        this.mapper = mapper;
        this.logger = logger;
    }
    
    public async Task<List<GetAllProductsResponse>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Get all products with category from database");
        
        var products = await this.repo.GetAllWithCategory(cancellationToken);

        return this.mapper.Map<List<GetAllProductsResponse>>(products);
    }
}