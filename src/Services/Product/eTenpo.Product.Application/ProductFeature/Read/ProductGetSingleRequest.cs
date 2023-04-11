using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Read;

public class ProductGetSingleRequest : IRequest<ProductGetSingleResponse>
{
    public Guid Id { get; set; }
    
    public ProductGetSingleRequest(Guid id)
    {
        this.Id = id;
    }
}