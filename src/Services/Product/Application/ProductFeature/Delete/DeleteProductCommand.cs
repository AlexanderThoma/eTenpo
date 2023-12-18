using eTenpo.Product.Application.CommandQueryAbstractions;
using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Delete;

public record DeleteProductCommand(Guid Id) : ICommand;