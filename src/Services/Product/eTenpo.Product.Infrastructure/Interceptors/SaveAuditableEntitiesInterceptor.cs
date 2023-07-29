using eTenpo.Product.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace eTenpo.Product.Infrastructure.Interceptors;

public class SaveAuditableEntitiesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        
        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var auditableEntries = dbContext.ChangeTracker.Entries<IAuditable>();

        foreach (var entry in auditableEntries)
        {
            if (entry.State is EntityState.Added)
            {
                entry.Property(x => x.CreatedOnUtc).CurrentValue = DateTime.UtcNow;
            }

            if (entry.State is EntityState.Modified)
            {
                entry.Property(x => x.ModifiedOnUtc).CurrentValue = DateTime.UtcNow;
            }
        }
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}