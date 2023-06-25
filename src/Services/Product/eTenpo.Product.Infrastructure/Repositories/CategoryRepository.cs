using eTenpo.Product.Domain.AggregateRoots.CategoryAggregate;
using eTenpo.Product.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace eTenpo.Product.Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> Exists(Guid id)
    {
        return await this.DbSet.AnyAsync(x => x.Id == id);
    }
}