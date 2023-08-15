namespace eTenpo.Product.Application.ProductFeature.Read.All;

public record GetAllProductsRequestResponse(Guid Id, string Name, decimal Price, string Description, int AvailableStock, Guid CategoryId)
    : ProductBaseRequestResponse(Id, Name, Price, Description, AvailableStock, CategoryId);