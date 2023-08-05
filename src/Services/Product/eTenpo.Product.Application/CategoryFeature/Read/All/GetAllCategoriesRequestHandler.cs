using eTenpo.Product.Application.CommandQueryAbstractions;

namespace eTenpo.Product.Application.CategoryFeature.Read.All;

public class GetAllCategoriesRequestHandler : IQueryHandler<GetAllCategoriesRequest, List<GetAllCategoriesRequestResponse>>
{
    public GetAllCategoriesRequestHandler()
    {
        
    }
    
    public async Task<List<GetAllCategoriesRequestResponse>> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}