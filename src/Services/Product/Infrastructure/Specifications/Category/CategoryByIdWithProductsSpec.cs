using eTenpo.Product.Infrastructure.Specifications.Base;

namespace eTenpo.Product.Infrastructure.Specifications.Category;

public class CategoryByIdWithProductsSpec : BaseSpecification<Domain.AggregateRoots.CategoryAggregate.Category>
{
    public CategoryByIdWithProductsSpec(Guid id) : base(category => category.Id == id)
    {
        this.AddInclude(x => x.Products);
    }
}