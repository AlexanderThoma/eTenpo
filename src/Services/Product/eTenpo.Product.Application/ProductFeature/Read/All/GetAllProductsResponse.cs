namespace eTenpo.Product.Application.ProductFeature.Read.All;

public record GetAllProductsResponse(Guid Id, string Name, decimal Price, string Description, int AvailableStock, Guid CategoryId)
    : ProductBaseResponse(Id, Name, Price, Description, AvailableStock, CategoryId);