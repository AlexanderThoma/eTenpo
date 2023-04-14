using eTenpo.Product.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace eTenpo.Product.Infrastructure.Repositories;

// TODO: use specification pattern
public class ProductRepository : IProductRepository
{
    private readonly DbSet<Domain.AggregateRoots.ProductAggregate.Product> productSet;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        this.productSet = dbContext.Set<Domain.AggregateRoots.ProductAggregate.Product>();
    }

    public async Task<Domain.AggregateRoots.ProductAggregate.Product?> GetByIdWithCategory(Guid id,
        CancellationToken cancellationToken = default)
    {
        return await this.productSet.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}