namespace eTenpo.Product.Domain.Contracts;

public interface IProductRepository : IRepository<AggregateRoots.ProductAggregate.Product>
{
    Task<Domain.AggregateRoots.ProductAggregate.Product?> FindByIdWithCategory(Guid id,
        CancellationToken cancellationToken = default);

    Task<List<Domain.AggregateRoots.ProductAggregate.Product>> GetAllByCategoryId(Guid categoryId,
        CancellationToken cancellationToken = default);

    Task<List<Domain.AggregateRoots.ProductAggregate.Product>> GetAllWithCategory(CancellationToken cancellationToken = default);
    Task<Guid> Add(Domain.AggregateRoots.ProductAggregate.Product product);
    void Delete(AggregateRoots.ProductAggregate.Product product);

    Task<Domain.AggregateRoots.ProductAggregate.Product?> FindById(Guid productId,
        CancellationToken cancellationToken = default);

    Task<Domain.AggregateRoots.ProductAggregate.Product?> FindByName(string name,
        CancellationToken cancellationToken = default);
}