using eTenpo.Product.Application.CommandQueryAbstractions;

namespace eTenpo.Product.Application.ProductFeature.Update;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    decimal Price,
    string Description,
    Guid CategoryId) : ICommand<UpdateProductCommandResponse>;
