namespace eTenpo.Product.Api.Requests.Product;

public abstract class ProductBaseRequest
{
    public string Name { get; }
    
    public decimal Price { get; }
    
    public string Description { get; }
    
    public int AvailableStock { get; }
    
    public Guid CategoryId { get; }
}