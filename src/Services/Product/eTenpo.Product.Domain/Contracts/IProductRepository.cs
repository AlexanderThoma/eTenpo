using eTenpo.Product.Domain.Common;

namespace eTenpo.Product.Domain.Contracts;

public interface IProductRepository : IRepository<AggregateRoots.ProductAggregate.Product>
{
    Task<Domain.AggregateRoots.ProductAggregate.Product?> GetByIdWithCategory(Guid id,
        CancellationToken cancellationToken = default);
}