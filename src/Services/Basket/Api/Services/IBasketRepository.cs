using eTenpo.Basket.Api.Models;

namespace eTenpo.Basket.Api.Services;

public interface IBasketRepository
{
    Task<BasketModel?> GetBasket(Guid customerId);
    Task<BasketModel> CreateBasket(BasketModel basket);
    Task<BasketModel> UpdateBasket(BasketModel updatedBasket);
    Task DeleteBasket(Guid customerId);
    ICollection<string> GetAllCustomers();
}
