using eTenpo.Basket.Api.Models;
using eTenpo.Basket.Api.Services;

namespace eTenpo.Basket.Api.Events.ProductPriceChangedEvent;

public class ProductPriceChangedEventHandler(IBasketRepository basketRepository)
{
    public async Task Handle(ProductPriceChangedEvent @event)
    {
        ArgumentNullException.ThrowIfNull(@event);
        
        // get all baskets
        var customerIds = basketRepository.GetAllCustomers();

        // iterate through all baskets
        foreach (var customerId in customerIds)
        {
            var basket = await basketRepository.GetBasket(Guid.Parse(customerId));
            if (basket is null)
            {
                continue;
            }
            
            await UpdatePriceInBasketItems(basket, @event.ProductId, @event.OldPrice, @event.NewPrice);
        }
    }

    private async Task UpdatePriceInBasketItems(BasketModel basket, Guid eventProductId, decimal eventOldPrice, decimal eventNewPrice)
    {
        var itemsToUpdate = basket.Items.Where(x => x.ProductId == eventProductId).ToList();

        foreach (var item in itemsToUpdate)
        {
            // check if current price and old price from event matches (if not, another price change occurred in between)
            if (item.UnitPrice == eventOldPrice)
            {
                item.OldUnitPrice = item.UnitPrice;
                item.UnitPrice = eventNewPrice;
            }
        }

        await basketRepository.UpdateBasket(basket);
    }
}
