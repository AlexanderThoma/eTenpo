using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Infrastructure.Specifications.Product;
using Microsoft.EntityFrameworkCore;

namespace eTenpo.Product.Infrastructure.Repositories;

public class ProductRepository(ApplicationDbContext dbContext)
    : BaseRepository<Domain.AggregateRoots.ProductAggregate.Product>(dbContext), IProductRepository
{
    public async Task<Domain.AggregateRoots.ProductAggregate.Product?> FindByName(string name,
        CancellationToken cancellationToken = default) =>
        await this.ApplySpecification(new ProductByNameSpec(name))
            .SingleOrDefaultAsync(cancellationToken);
    
    public async Task<Domain.AggregateRoots.ProductAggregate.Product?> FindById(Guid productId,
        CancellationToken cancellationToken = default) =>
        await this.ApplySpecification(new ProductByIdSpec(productId))
            .SingleOrDefaultAsync(cancellationToken);
    
    public async Task<Domain.AggregateRoots.ProductAggregate.Product?> FindByIdWithCategory(Guid productId,
        CancellationToken cancellationToken = default) =>
        await this.ApplySpecification(new ProductByIdWithCategorySpec(productId))
        .SingleOrDefaultAsync(cancellationToken);
    
    public async Task<List<Domain.AggregateRoots.ProductAggregate.Product>> GetAllByCategoryId(Guid categoryId,
        CancellationToken cancellationToken = default) =>
        await this.ApplySpecification(new ProductsByCategoryIdSpec(categoryId))
            .ToListAsync(cancellationToken);
    
    public async Task<List<Domain.AggregateRoots.ProductAggregate.Product>> GetAllWithCategory(CancellationToken cancellationToken = default) =>
        await this.ApplySpecification(new ProductsWithCategorySpec())
            .ToListAsync(cancellationToken);

    public async Task<Guid> Add(Domain.AggregateRoots.ProductAggregate.Product product) => (await this.DbSet.AddAsync(product)).Entity.Id;
    
    public void Delete(Domain.AggregateRoots.ProductAggregate.Product product) => this.DbSet.Remove(product);
    
    // Update not necessary -> to be done via domain model (tracked by EF core), then just use saveChanges
}