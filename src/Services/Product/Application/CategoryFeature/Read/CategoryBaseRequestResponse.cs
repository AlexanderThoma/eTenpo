namespace eTenpo.Product.Application.CategoryFeature.Read;

public abstract record CategoryBaseRequestResponse(Guid Id,
    string Name,
    string Description);