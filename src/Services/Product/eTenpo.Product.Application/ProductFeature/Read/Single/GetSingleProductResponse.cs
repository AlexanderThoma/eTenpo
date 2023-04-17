namespace eTenpo.Product.Application.ProductFeature.Read.Single;

public record GetSingleProductResponse(Guid Id, string Name, decimal Price, string Description, int AvailableStock, Guid CategoryId)
    : ProductBaseResponse(Id, Name, Price, Description, AvailableStock, CategoryId);