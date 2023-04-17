namespace eTenpo.Product.Domain.Contracts;

public interface IProductRepository : IRepository<AggregateRoots.ProductAggregate.Product>
{
    Task<Domain.AggregateRoots.ProductAggregate.Product?> GetByIdWithCategory(Guid id,
        CancellationToken cancellationToken = default);

    Task<List<Domain.AggregateRoots.ProductAggregate.Product>> GetAllByCategoryId(Guid categoryId,
        CancellationToken cancellationToken = default);

    Task<List<Domain.AggregateRoots.ProductAggregate.Product>> GetAllWithCategory(CancellationToken cancellationToken = default);
    Task<Guid> Add(Domain.AggregateRoots.ProductAggregate.Product product);
    void Delete(AggregateRoots.ProductAggregate.Product product);
}