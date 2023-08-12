using eTenpo.Product.Infrastructure.Specifications.Base;

namespace eTenpo.Product.Infrastructure.Specifications.Product;

public class ProductByNameSpec : BaseSpecification<Domain.AggregateRoots.ProductAggregate.Product>
{
    public ProductByNameSpec(string name) :
        base(product => product.ProductName.Value == name)
    {
        SetAsNoTracking();
    }
}