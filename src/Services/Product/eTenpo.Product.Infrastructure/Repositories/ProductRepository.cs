using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace eTenpo.Product.Infrastructure.Repositories;

public class ProductRepository : BaseRepository<Domain.AggregateRoots.ProductAggregate.Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Domain.AggregateRoots.ProductAggregate.Product?> GetByIdWithCategory(Guid productId,
        CancellationToken cancellationToken = default) =>
        await this.ApplySpecification(new ProductByIdWithCategorySpec(productId))
        .SingleOrDefaultAsync(cancellationToken);
    
    public async Task<List<Domain.AggregateRoots.ProductAggregate.Product>> GetAllByCategoryId(Guid categoryId,
        CancellationToken cancellationToken = default) =>
        await this.ApplySpecification(new ProductsByCategoryIdSpec(categoryId))
            .ToListAsync(cancellationToken);
}