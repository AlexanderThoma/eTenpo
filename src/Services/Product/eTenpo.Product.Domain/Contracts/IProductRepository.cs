using eTenpo.Product.Domain.Common;

namespace eTenpo.Product.Domain.Contracts;

public interface IProductRepository : IRepository<AggregateRoots.ProductAggregate.Product>
{
}