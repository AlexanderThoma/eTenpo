using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Update;

public class ProductUpdateCommand : IRequest<ProductUpdateResponse>
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public decimal Price { get; set; }
    
    public string Description { get; set; }
    
    public Guid CategoryId { get; set; }
}