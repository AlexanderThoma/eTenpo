namespace eTenpo.Product.Application.CategoryFeature.Read.All;

public record GetAllCategoriesRequestResponse(Guid Id,
    string Name,
    string Description) : CategoryBaseRequestResponse(Id, Name, Description);