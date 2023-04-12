using eTenpo.Product.Domain.Common;

namespace eTenpo.Product.Domain.DomainEvents;

public record ProductDeletedEvent(Guid ProductId) : DomainEvent;