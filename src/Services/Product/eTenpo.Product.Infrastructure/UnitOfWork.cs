using eTenpo.Product.Domain.Contracts;

namespace eTenpo.Product.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // TODO: add outbox pattern
        // TODO: add auditMessages (IAuditable), CreatedBy/CreatedOn
        
        _ = await this.dbContext.SaveChangesAsync(cancellationToken);
    }
}