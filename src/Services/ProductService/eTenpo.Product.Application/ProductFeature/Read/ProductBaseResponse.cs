namespace eTenpo.Product.Application.ProductFeature.Read;

public abstract record ProductBaseResponse(Guid Id,
    string Name,
    decimal Price,
    string Description,
    int AvailableStock,
    Guid CategoryId);