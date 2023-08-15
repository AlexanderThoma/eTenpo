namespace eTenpo.Product.Api.Requests.Product;

public record UpdateProductRequest(
    string Name,
    decimal Price,
    string Description,
    Guid CategoryId) : ProductBaseRequest(Name, Price, Description, CategoryId);