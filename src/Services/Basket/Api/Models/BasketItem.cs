namespace eTenpo.Basket.Api.Models;

public record BasketItem(
    Guid Id,
    Guid ProductId,
    decimal UnitPrice,
    decimal OldUnitPrice,
    int Quantity
);
