using eTenpo.Product.Domain.Common;
using eTenpo.Product.Infrastructure.Specifications.Base;
using Microsoft.EntityFrameworkCore;

namespace eTenpo.Product.Infrastructure.Repositories;

public abstract class BaseRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
{
    protected BaseRepository(ApplicationDbContext dbContext)
    {
        this.DbSet = dbContext.Set<TAggregateRoot>();
    }
    
    protected DbSet<TAggregateRoot> DbSet { get; }
    
    protected IQueryable<TAggregateRoot> ApplySpecification(
        BaseSpecification<TAggregateRoot> specification)
    {
        return SpecificationEvaluator.GetQuery(this.DbSet, specification);
    }
}