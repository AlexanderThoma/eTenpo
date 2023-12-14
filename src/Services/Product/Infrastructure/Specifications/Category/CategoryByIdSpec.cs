using eTenpo.Product.Infrastructure.Specifications.Base;

namespace eTenpo.Product.Infrastructure.Specifications.Category;

public class CategoryByIdSpec(Guid id)
    : BaseSpecification<Domain.AggregateRoots.CategoryAggregate.Category>(category => category.Id == id);