using eTenpo.Product.Infrastructure.Specifications.Base;

namespace eTenpo.Product.Infrastructure.Specifications.Category;

public class CategoryByNameSpec : BaseSpecification<Domain.AggregateRoots.CategoryAggregate.Category>
{
    public CategoryByNameSpec(string name) : base(category => category.Name.Value == name)
    {
        SetAsNoTracking();
    }
}