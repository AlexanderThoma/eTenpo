using eTenpo.Product.Domain.Common;

namespace eTenpo.Product.Domain.DomainEvents;

public record ProductPriceUpdatedEvent(Guid ProductId, decimal OldPrice, decimal NewPrice) : DomainEvent;