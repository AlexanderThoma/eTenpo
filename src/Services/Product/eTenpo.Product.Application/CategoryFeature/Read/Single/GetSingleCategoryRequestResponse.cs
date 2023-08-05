namespace eTenpo.Product.Application.CategoryFeature.Read.Single;

public record GetSingleCategoryRequestResponse(Guid Id,
    string Name,
    string Description) : CategoryBaseRequestResponse(Id, Name, Description);