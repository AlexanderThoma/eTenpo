namespace eTenpo.Product.Application.ProductFeature.Read;

public record GetSingleProductResponse(Guid Id, string Name, decimal Price, string Description, int AvailableStock, Guid CategoryId)
    : ProductBaseResponse(Id, Name, Price, Description, AvailableStock, CategoryId);