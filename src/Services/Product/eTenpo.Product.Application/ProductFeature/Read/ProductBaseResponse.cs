namespace eTenpo.Product.Application.ProductFeature.Read;

public abstract class ProductBaseResponse
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public decimal Price { get; set; }
    
    public string Description { get; set; }
    
    public int AvailableStock { get; set; }
    
    public Guid CategoryId { get; set; }
}