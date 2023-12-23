namespace eTenpo.Basket.Api.Events.ProductPriceChangedEvent;

public record ProductPriceChangedEvent(
    Guid ProductId,
    decimal OldPrice,
    decimal NewPrice,
    Guid EventId,
    DateTime OccurredOn);
