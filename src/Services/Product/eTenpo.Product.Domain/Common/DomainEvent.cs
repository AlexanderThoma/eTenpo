using MediatR;

namespace eTenpo.Product.Domain.Common;

public abstract class DomainEvent : INotification
{
    public Guid EventId { get; set; } = Guid.NewGuid();

    public DateTime OccurredOn { get; set; } = DateTime.UtcNow;
}