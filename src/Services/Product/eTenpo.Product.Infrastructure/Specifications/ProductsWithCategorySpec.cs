using eTenpo.Product.Infrastructure.Specifications.Base;

namespace eTenpo.Product.Infrastructure.Specifications;

public class ProductsWithCategorySpec : BaseSpecification<Domain.AggregateRoots.ProductAggregate.Product>
{
    public ProductsWithCategorySpec() : base(null)
    {
        // TODO: uncomment after adding categories
        //this.AddInclude(x => x.Category!);
    }
}