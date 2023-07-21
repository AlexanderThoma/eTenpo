using eTenpo.Product.Infrastructure.Specifications.Base;

namespace eTenpo.Product.Infrastructure.Specifications;

public class ProductByIdWithCategorySpec : BaseSpecification<Domain.AggregateRoots.ProductAggregate.Product>
{
    public ProductByIdWithCategorySpec(Guid productId) :
        base(product => product.Id == productId)
    {
        // TODO: uncomment after adding categories
        //ude(x => x.Category!);
    }
}