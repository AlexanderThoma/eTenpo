using eTenpo.Basket.Api.Models;
using eTenpo.Basket.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace eTenpo.Basket.Api.Endpoints;

public static class EndpointsExtension
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var basketGroup = app.MapGroup("api/v1/basket");
        basketGroup.MapPost("", async ([FromBody] BasketModel basket, IBasketRepository basketRepository) =>
            {
                var resultBasket = await basketRepository.CreateBasket(basket);
                
                return Results.CreatedAtRoute("GetBasket", new { customerId = resultBasket.CustomerId });
            })
            .WithName("CreateBasket")
            .WithOpenApi();
        
        basketGroup.MapGet("{customerId:guid}", async ([FromRoute] Guid customerId, IBasketRepository basketRepository) =>
            {
                var basket = await basketRepository.GetBasket(customerId);
                return basket;
            })
            .WithName("GetBasket")
            .WithOpenApi();
        
        basketGroup.MapPut("{customerId:guid}", async ([FromRoute] Guid customerId, [FromBody]BasketModel basket, IBasketRepository basketRepository) =>
            {
                var updatedBasket = await basketRepository.UpdateBasket(basket);
                return Results.Ok(updatedBasket);
            })
            .WithName("UpdateBasket")
            .WithOpenApi();
        
        basketGroup.MapDelete("{customerId:guid}", async ([FromRoute] Guid customerId, IBasketRepository basketRepository) =>
            {
                await basketRepository.DeleteBasket(customerId);
                return Results.Ok();
            })
            .WithName("DeleteBasket")
            .WithOpenApi();
        
        return app;
    }
}
