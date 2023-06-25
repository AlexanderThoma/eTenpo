using eTenpo.Product.Domain.AggregateRoots.CategoryAggregate;
using eTenpo.Product.Domain.Common;

namespace eTenpo.Product.Domain.Contracts;

public interface ICategoryRepository : IRepository<Category>
{
    Task<bool> Exists(Guid id);
}