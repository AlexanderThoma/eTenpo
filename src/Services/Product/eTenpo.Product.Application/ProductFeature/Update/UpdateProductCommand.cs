using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Update;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    decimal Price,
    string Description,
    Guid CategoryId) : IRequest<UpdateProductResponse>
{
    // used for mapping the id in the controller
    public Guid Id { get; set; } = Id;
}
