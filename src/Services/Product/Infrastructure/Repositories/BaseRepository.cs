using eTenpo.Product.Domain.Common;
using eTenpo.Product.Infrastructure.Specifications.Base;
using Microsoft.EntityFrameworkCore;

namespace eTenpo.Product.Infrastructure.Repositories;

public abstract class BaseRepository<TAggregateRoot>(ApplicationDbContext dbContext)
    where TAggregateRoot : AggregateRoot
{
    protected DbSet<TAggregateRoot> DbSet { get; } = dbContext.Set<TAggregateRoot>();

    protected IQueryable<TAggregateRoot> ApplySpecification(
        BaseSpecification<TAggregateRoot> specification)
    {
        return SpecificationEvaluator.GetQuery(this.DbSet, specification);
    }
}