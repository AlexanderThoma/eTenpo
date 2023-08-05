using eTenpo.Product.Application.CommandQueryAbstractions;

namespace eTenpo.Product.Application.CategoryFeature.Read.Single;

public class GetSingleCategoryRequestHandler : IQueryHandler<GetSingleCategoryRequest, GetSingleCategoryRequestResponse>
{
    public GetSingleCategoryRequestHandler()
    {
        
    }
    
    public async Task<GetSingleCategoryRequestResponse> Handle(GetSingleCategoryRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}