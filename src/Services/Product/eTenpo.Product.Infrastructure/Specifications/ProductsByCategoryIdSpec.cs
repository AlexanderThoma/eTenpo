using eTenpo.Product.Infrastructure.Specifications.Base;

namespace eTenpo.Product.Infrastructure.Specifications;

public class ProductsByCategoryIdSpec : BaseSpecification<Domain.AggregateRoots.ProductAggregate.Product>
{
    public ProductsByCategoryIdSpec(Guid categoryId) : base(product => product.CategoryId.Value == categoryId)
    {
    }
}