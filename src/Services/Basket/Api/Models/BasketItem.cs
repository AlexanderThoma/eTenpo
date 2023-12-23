namespace eTenpo.Basket.Api.Models;

public class BasketItem(
    Guid id,
    Guid productId,
    decimal unitPrice,
    decimal oldUnitPrice,
    int quantity
)
{
    public Guid Id { get; } = id;
    public Guid ProductId { get; } = productId;
    public decimal UnitPrice { get; set; } = unitPrice;
    public decimal OldUnitPrice { get; set; } = oldUnitPrice;
    public int Quantity { get; } = quantity;
}
