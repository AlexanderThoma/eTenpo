using MediatR;

namespace eTenpo.Product.Api.Application.ProductFeature.Delete;

public record ProductDeleteCommand(Guid Id) : IRequest<ProductDeleteResponse>;