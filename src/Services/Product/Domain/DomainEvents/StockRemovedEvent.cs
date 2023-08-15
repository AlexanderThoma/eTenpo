using eTenpo.Product.Domain.Common;

namespace eTenpo.Product.Domain.DomainEvents;

public record StockRemovedEvent(Guid ProductId, int OldStock, int NewStock) : DomainEvent;