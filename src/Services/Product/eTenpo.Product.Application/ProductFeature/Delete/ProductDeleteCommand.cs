using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Delete;

public record ProductDeleteCommand(Guid Id) : IRequest<ProductDeleteResponse>;