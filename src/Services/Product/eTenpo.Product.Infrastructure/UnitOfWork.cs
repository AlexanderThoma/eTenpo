using eTenpo.Product.Domain.Common;
using eTenpo.Product.Domain.Contracts;
using eTenpo.Product.Infrastructure.Outbox;
using Newtonsoft.Json;

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
        // alternative outbox pattern: https://www.kamilgrzybek.com/blog/posts/the-outbox-pattern
        
        var events = this.dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(x => x.Entity)
            .SelectMany(aggregateRoot =>
            {
                var domainEvents = aggregateRoot.DomainEvents;
                aggregateRoot.ClearDomainEvents();

                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage
            {
                OccurredOnUtc = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                Content = JsonConvert.SerializeObject(domainEvent, new JsonSerializerSettings
                {
                    // will return full type in json to restore later to domain event instance
                    TypeNameHandling = TypeNameHandling.All
                })
            })
            .ToList();
        
        await dbContext.Set<OutboxMessage>().AddRangeAsync(events, cancellationToken);
        
        _ = await this.dbContext.SaveChangesAsync(cancellationToken);
    }
}