using eTenpo.Product.Application.CommandQueryAbstractions;

namespace eTenpo.Product.Application.ProductFeature.Read.All;

public record GetAllProductsRequest : IQuery<List<GetAllProductsResponse>>;