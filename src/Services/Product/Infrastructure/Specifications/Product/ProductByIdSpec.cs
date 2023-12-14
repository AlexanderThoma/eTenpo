using eTenpo.Product.Infrastructure.Specifications.Base;

namespace eTenpo.Product.Infrastructure.Specifications.Product;

public class ProductByIdSpec(Guid productId)
    : BaseSpecification<Domain.AggregateRoots.ProductAggregate.Product>(product => product.Id == productId);