using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Update;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    decimal Price,
    string Description,
    Guid CategoryId) : IRequest<UpdateProductResponse>
{
    public Guid Id { get; set; } = Id;
}
