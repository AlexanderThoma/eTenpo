using eTenpo.Product.Application.CommandQueryAbstractions;
using eTenpo.Product.Domain.Contracts;
using Microsoft.Extensions.Logging;

namespace eTenpo.Product.Application.CategoryFeature.Read.All;

public class GetAllCategoriesRequestHandler(
    ICategoryRepository repository,
    ILogger<GetAllCategoriesRequestHandler> logger)
    : IQueryHandler<GetAllCategoriesRequest, List<GetAllCategoriesRequestResponse>>
{
    public async Task<List<GetAllCategoriesRequestResponse>> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get all products with category from database");
        
        var categories = await repository.GetAll(cancellationToken);

        return categories.Select(x => new GetAllCategoriesRequestResponse(x.Id, x.Name, x.Description)).ToList();
    }
}