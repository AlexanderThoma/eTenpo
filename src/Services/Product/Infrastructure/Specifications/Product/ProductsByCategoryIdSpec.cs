using eTenpo.Product.Infrastructure.Specifications.Base;

namespace eTenpo.Product.Infrastructure.Specifications.Product;

public class ProductsByCategoryIdSpec(Guid categoryId)
    : BaseSpecification<Domain.AggregateRoots.ProductAggregate.Product>(product => product.CategoryId == categoryId);