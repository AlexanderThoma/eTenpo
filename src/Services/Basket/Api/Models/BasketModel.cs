namespace eTenpo.Basket.Api.Models;

public class BasketModel
{
    public Guid CustomerId { get; init; }

    public List<BasketItem> Items { get; } = [];
}
