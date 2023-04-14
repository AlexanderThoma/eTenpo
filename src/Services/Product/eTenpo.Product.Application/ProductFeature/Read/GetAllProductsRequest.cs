using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Read;

public record GetAllProductsRequest : IRequest<List<GetAllProductsResponse>>;