namespace eTenpo.Product.Application.ProductFeature.Update;

public record UpdateProductCommandResponse(Guid Id,
    string Name,
    decimal Price,
    string Description,
    int AvailableStock,
    Guid CategoryId);