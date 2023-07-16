namespace eTenpo.Product.Api.Requests.Product;

public record CreateProductRequest(string Name, decimal Price, string Description, int AvailableStock, Guid CategoryId)
    : ProductBaseRequest(Name, Price, Description, AvailableStock, CategoryId);