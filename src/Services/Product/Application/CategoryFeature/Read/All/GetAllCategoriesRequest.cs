using eTenpo.Product.Application.CommandQueryAbstractions;

namespace eTenpo.Product.Application.CategoryFeature.Read.All;

public record GetAllCategoriesRequest : IQuery<List<GetAllCategoriesRequestResponse>>;