namespace eTenpo.Product.Application.ProductFeature.Read;

public abstract record ProductBaseRequestResponse(Guid Id,
    string Name,
    decimal Price,
    string Description,
    int AvailableStock,
    Guid CategoryId);