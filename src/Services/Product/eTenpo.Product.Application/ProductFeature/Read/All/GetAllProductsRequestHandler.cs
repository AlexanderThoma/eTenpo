using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.Contracts;
using Microsoft.Extensions.Logging;

namespace eTenpo.Product.Application.ProductFeature.Read.All;

public class GetAllProductsRequestHandler : IQueryHandler<GetAllProductsRequest, List<GetAllProductsRequestResponse>>
{
    private readonly IProductRepository repo;
    private readonly ILogger<GetAllProductsRequestHandler> logger;

    public GetAllProductsRequestHandler(IProductRepository repo, ILogger<GetAllProductsRequestHandler> logger)
    {
        this.repo = repo;
        this.logger = logger;
    }
    
    public async Task<List<GetAllProductsRequestResponse>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Get all products with category from database");
        
        var products = await this.repo.GetAllWithCategory(cancellationToken);

        return products.Select(x => new GetAllProductsRequestResponse(
                x.Id,
                x.ProductName,
                x.Price,
                x.ProductDescription,
                x.AvailableStock,
                x.CategoryId))
            .ToList();
    }
}