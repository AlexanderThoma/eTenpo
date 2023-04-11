using MediatR;

namespace eTenpo.Product.Application.ProductFeature.Read;

public class ProductGetAllRequest : IRequest<List<ProductGetAllResponse>>
{
}