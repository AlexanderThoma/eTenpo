using eTenpo.Product.Infrastructure.Specifications.Base;

namespace eTenpo.Product.Infrastructure.Specifications;

public class ProductByNameSpec : BaseSpecification<Domain.AggregateRoots.ProductAggregate.Product>
{
    public ProductByNameSpec(string name) :
        base(product => product.Name.Value == name)
    {
        SetAsNoTracking();
    }
}