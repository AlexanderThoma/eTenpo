using eTenpo.Product.Application.CommandQueryAbstractions;

namespace eTenpo.Product.Application.ProductFeature.Create;

public record CreateProductCommand(
    string Name,
    decimal Price,
    string Description,
    int AvailableStock,
    Guid CategoryId) : ICommand<CreateProductCommandResponse>;