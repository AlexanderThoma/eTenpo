using eTenpo.Product.Domain.AggregateRoots.CategoryAggregate;
using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Infrastructure.Specifications.Category;
using Microsoft.EntityFrameworkCore;

namespace eTenpo.Product.Infrastructure.Repositories;

public class CategoryRepository(ApplicationDbContext dbContext)
    : BaseRepository<Category>(dbContext), ICategoryRepository
{
    public async Task<bool> Exists(Guid id) => await this.DbSet.AnyAsync(x => x.Id == id);
    
    public async Task<Category?> FindById(Guid id, CancellationToken cancellationToken = default) => await this.ApplySpecification(new CategoryByIdSpec(id))
        .SingleOrDefaultAsync(cancellationToken);
    
    public async Task<Category?> FindByIdWithProducts(Guid id, CancellationToken cancellationToken = default) => await this.ApplySpecification(new CategoryByIdWithProductsSpec(id))
        .SingleOrDefaultAsync(cancellationToken);

    public async Task<List<Category>> GetAll(CancellationToken cancellationToken = default) => await this.DbSet.ToListAsync(cancellationToken);

    public async Task<Guid> Add(Category category) => (await this.DbSet.AddAsync(category)).Entity.Id;

    public void Delete(Category category) => this.DbSet.Remove(category);

    public async Task<Category?> FindByName(string name, CancellationToken cancellationToken = default) => await this.ApplySpecification(new CategoryByNameSpec(name))
        .SingleOrDefaultAsync(cancellationToken);
}