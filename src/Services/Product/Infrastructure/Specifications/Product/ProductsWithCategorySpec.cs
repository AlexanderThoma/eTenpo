using eTenpo.Product.Infrastructure.Specifications.Base;

namespace eTenpo.Product.Infrastructure.Specifications.Product;

public class ProductsWithCategorySpec : BaseSpecification<Domain.AggregateRoots.ProductAggregate.Product>
{
    public ProductsWithCategorySpec() : base(null)
    {
        this.AddInclude(x => x.Category!);
    }
}