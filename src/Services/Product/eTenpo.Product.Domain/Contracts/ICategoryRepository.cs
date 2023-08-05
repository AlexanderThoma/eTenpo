using eTenpo.Product.Domain.AggregateRoots.CategoryAggregate;

namespace eTenpo.Product.Domain.Contracts;

public interface ICategoryRepository : IRepository<Category>
{
    Task<bool> Exists(Guid id);
    
    Task<Category?> FindById(Guid id, CancellationToken cancellationToken = default);
    
    Task<List<Category>> GetAll(CancellationToken cancellationToken = default);
    
    Task<Guid> Add(Category category);
    
    void Delete(Category category);

    Task<Category?> FindByName(string name, CancellationToken cancellationToken = default);
}