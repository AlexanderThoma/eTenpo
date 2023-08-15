using eTenpo.Product.Infrastructure.Specifications.Base;

namespace eTenpo.Product.Infrastructure.Specifications.Product;

public class ProductByIdSpec : BaseSpecification<Domain.AggregateRoots.ProductAggregate.Product>
{
    public ProductByIdSpec(Guid productId) :
        base(product => product.Id == productId)
    {
    }
}