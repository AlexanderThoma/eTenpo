using eTenpo.Product.Application.CommandQueryAbstractions;

namespace eTenpo.Product.Application.CategoryFeature.Read.Single;

public record GetSingleCategoryRequest(Guid Id) : IQuery<GetSingleCategoryRequestResponse>;