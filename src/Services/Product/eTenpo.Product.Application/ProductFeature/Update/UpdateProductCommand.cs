using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Update;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    decimal Price,
    string Description,
    int AvailableStock,
    Guid CategoryId) : IRequest<UpdateProductResponse>
{
    public Guid Id { get; set; } = Id;
}
