using eTenpo.Product.Domain.Common;

namespace eTenpo.Product.Domain.DomainEvents;

public record ProductCreatedEvent(AggregateRoots.ProductAggregate.Product Product) : DomainEvent;