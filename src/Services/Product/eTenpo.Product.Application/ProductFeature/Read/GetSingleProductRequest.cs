using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Read;

public record GetSingleProductRequest(Guid Id) : IRequest<GetSingleProductResponse>;