using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Create;

public class ProductCreateCommand : IRequest<ProductCreateResponse>
{
    public string Name { get; set; }
    
    public decimal Price { get; set; }
    
    public string Description { get; set; }
    
    public int AvailableStock { get; set; }
    
    public Guid CategoryId { get; set; }
}