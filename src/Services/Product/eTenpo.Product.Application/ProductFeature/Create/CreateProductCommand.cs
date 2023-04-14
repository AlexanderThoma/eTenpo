using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Create;

public record CreateProductCommand(
    string Name,
    decimal Price,
    string Description,
    int AvailableStock,
    Guid CategoryId) : IRequest<CreateProductCommandResponse>;