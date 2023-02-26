using MediatR;

namespace eTenpo.Product.Domain.Common;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot(Guid id) : base(id)
    {
    }
    
    private readonly List<INotification> domainEvents = new();
    
    public IReadOnlyCollection<INotification> DomainEvents => this.domainEvents.AsReadOnly();

    public void AddDomainEvent(INotification domainEvent)
    {
        domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(INotification domainEvent)
    {
        domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        domainEvents.Clear();
    }
}