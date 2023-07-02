namespace eTenpo.Product.Api.Dtos.Product;

public abstract class ProductBaseRequest
{
    public string Name { get; set; }
    
    public decimal Price { get; set; }
    
    public string Description { get; set; }
    
    public int AvailableStock { get; set; }
    
    public Guid CategoryId { get; set; }
}