using eTenpo.Product.Infrastructure.Specifications.Base;

namespace eTenpo.Product.Infrastructure.Specifications.Product;

public class ProductsByCategoryIdSpec : BaseSpecification<Domain.AggregateRoots.ProductAggregate.Product>
{
    public ProductsByCategoryIdSpec(Guid categoryId) : base(product => product.CategoryId == categoryId)
    {
    }
}