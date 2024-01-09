using System.Collections.Concurrent;
using System.Text.Json;
using eTenpo.Basket.Api.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace eTenpo.Basket.Api.Services;

public class BasketRepository(IDistributedCache cache) : IBasketRepository
{
    private static readonly ConcurrentDictionary<string, bool> CacheKeys = new();
    
    public async Task<BasketModel?> GetBasket(Guid customerId)
    {
        var result = await cache.GetStringAsync(customerId.ToString());

        return string.IsNullOrWhiteSpace(result) ? null : JsonSerializer.Deserialize(result, BasketModelContext.Default.BasketModel);
    }

    public async Task<BasketModel> CreateBasket(BasketModel basket)
    {
        ArgumentNullException.ThrowIfNull(basket);

        var basketString = JsonSerializer.Serialize(basket, BasketModelContext.Default.BasketModel);
        
        await cache.SetStringAsync(basket.CustomerId.ToString(), basketString);
        CacheKeys.TryAdd(basket.CustomerId.ToString(), true);
        
        return basket;
    }

    public async Task<BasketModel> UpdateBasket(BasketModel updatedBasket)
    {
        ArgumentNullException.ThrowIfNull(updatedBasket);
        
        await this.DeleteBasket(updatedBasket.CustomerId);
        await this.CreateBasket(updatedBasket);

        return updatedBasket;
    }

    public async Task DeleteBasket(Guid customerId)
    {
        await cache.RemoveAsync(customerId.ToString());
        CacheKeys.TryRemove(new KeyValuePair<string, bool>(customerId.ToString(), true));
    }

    public ICollection<string> GetAllCustomers()
    {
        return CacheKeys.Keys;
    }
}
