namespace eTenpo.Product.Api.Dtos.Product;

public class UpdateProductDto
{
    public string Name { get; set; }
    
    public decimal Price { get; set; }
    
    public string Description { get; set; }
    
    public Guid CategoryId { get; set; }
}