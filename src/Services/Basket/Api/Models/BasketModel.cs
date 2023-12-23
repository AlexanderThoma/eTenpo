namespace eTenpo.Basket.Api.Models;

public record BasketModel(Guid CustomerId, List<BasketItem> Items);
