using eTenpo.Product.Infrastructure.Specifications.Base;

namespace eTenpo.Product.Infrastructure.Specifications.Category;

public class CategoryByIdSpec : BaseSpecification<Domain.AggregateRoots.CategoryAggregate.Category>
{
    public CategoryByIdSpec(Guid id) : base(category => category.Id == id)
    {
    }
}