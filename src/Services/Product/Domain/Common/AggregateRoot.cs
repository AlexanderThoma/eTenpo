using MediatR;

namespace eTenpo.Product.Domain.Common;

public abstract class AggregateRoot : Entity
{
    private readonly List<INotification> domainEvents = new();
    
    public IReadOnlyCollection<INotification> DomainEvents => this.domainEvents.AsReadOnly();

    protected void AddDomainEvent(INotification domainEvent) => domainEvents.Add(domainEvent);

    public void RemoveDomainEvent(INotification domainEvent) => domainEvents.Remove(domainEvent);

    public void ClearDomainEvents() => domainEvents.Clear();
}