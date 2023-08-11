namespace eTenpo.Product.Application.ProductFeature.Create;

public record CreateProductCommandResponse(Guid Id,
    string Name,
    decimal Price,
    string Description,
    int AvailableStock,
    Guid CategoryId);