namespace eTenpo.Product.Api.Requests.Product;

public class UpdateProductRequest
{
    public string Name { get; }
    
    public decimal Price { get; }
    
    public string Description { get; }

    public Guid CategoryId { get; }
}