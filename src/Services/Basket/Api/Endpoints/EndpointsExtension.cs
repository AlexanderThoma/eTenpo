using Asp.Versioning.Builder;
using eTenpo.Basket.Api.EndpointFilters;
using eTenpo.Basket.Api.Models;
using eTenpo.Basket.Api.Services;
using eTenpo.Basket.Api.Validators;
using Microsoft.AspNetCore.Mvc;

namespace eTenpo.Basket.Api.Endpoints;

public static class EndpointsExtension
{
    public static WebApplication MapEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        var basketGroup = app.MapGroup("/api/v{version:apiVersion}/basket")
            .WithOpenApi()
            .WithApiVersionSet(apiVersionSet);
        
        basketGroup.MapPost("/",
                async ([FromBody] BasketModel basket, IBasketRepository basketRepository) =>
                {
                    var resultBasket = await basketRepository.CreateBasket(basket);

                    /*
                     * Todo:
                     * - implement http request logging
                     * - implement correlation ID middleware
                     */

                    return Results.CreatedAtRoute("GetBasket", new { customerId = resultBasket.CustomerId });
                })
            .WithName("CreateBasket")
            .AddEndpointFilter<ValidationFilter<BasketModel>>();

        basketGroup.MapGet("/{customerId:guid}",
                async ([FromRoute, RequiredAndNotEmpty] Guid customerId, IBasketRepository basketRepository) =>
                {
                    /*
                     * Todo:
                     * - error if basket of given customerId does not exist
                     * - use same error format as in product service
                     * - implement http request logging
                     * - implement correlation ID middleware
                     */

                    var basket = await basketRepository.GetBasket(customerId);

                    if (basket is null)
                    {
                        var problemDetails = new ProblemDetails
                        {
                            Status = StatusCodes.Status404NotFound,
                            Type = "NotFoundException",
                            Title = "The requested entity could not be found",
                            Detail = $"The basket with customer id \"{customerId}\" could not be found",
                            Instance = ""
                        };

                        return Results.NotFound(problemDetails);
                    }

                    return Results.Ok(basket);
                })
            .WithName("GetBasket");
        
        basketGroup.MapPut("/{customerId:guid}",
                async ([FromRoute, RequiredAndNotEmpty] Guid customerId, [FromBody] BasketModel basket,
                    IBasketRepository basketRepository) =>
                {
                    /*
                     * Todo:
                     * - error if basket of given customerId does not exist
                     * - use same error format as in product service
                     * - implement http request logging
                     * - implement correlation ID middleware
                     */

                    var currentBasket = await basketRepository.GetBasket(customerId);

                    if (currentBasket is null)
                    {
                        var problemDetails = new ProblemDetails
                        {
                            Status = StatusCodes.Status404NotFound,
                            Type = "NotFoundException",
                            Title = "The requested entity could not be found",
                            Detail = $"The basket with customer id \"{customerId}\" could not be found",
                            Instance = ""
                        };

                        return Results.NotFound(problemDetails);
                    }
                    
                    var updatedBasket = await basketRepository.UpdateBasket(basket);
                    return Results.Ok(updatedBasket);
                })
            .WithName("UpdateBasket")
            .AddEndpointFilter<ValidationFilter<BasketModel>>();

        basketGroup.MapDelete("/{customerId:guid}",
                async ([FromRoute, RequiredAndNotEmpty] Guid customerId, IBasketRepository basketRepository) =>
                {
                    /*
                     * Todo:
                     * - use same error format as in product service
                     * - implement http request logging
                     * - implement correlation ID middleware
                     */
                    
                    var basket = await basketRepository.GetBasket(customerId);

                    if (basket is null)
                    {
                        var problemDetails = new ProblemDetails
                        {
                            Status = StatusCodes.Status404NotFound,
                            Type = "NotFoundException",
                            Title = "The requested entity could not be found",
                            Detail = $"The basket with customer id \"{customerId}\" could not be found",
                            Instance = ""
                        };

                        return Results.NotFound(problemDetails);
                    }

                    await basketRepository.DeleteBasket(customerId);
                    return Results.NoContent();
                })
            .WithName("DeleteBasket");
        
        return app;
    }
}
