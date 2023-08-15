using eTenpo.Product.Infrastructure.Specifications.Base;

namespace eTenpo.Product.Infrastructure.Specifications.Product;

public class ProductByIdWithCategorySpec : BaseSpecification<Domain.AggregateRoots.ProductAggregate.Product>
{
    public ProductByIdWithCategorySpec(Guid productId) :
        base(product => product.Id == productId)
    {
        this.AddInclude(x => x.Category!);
    }
}