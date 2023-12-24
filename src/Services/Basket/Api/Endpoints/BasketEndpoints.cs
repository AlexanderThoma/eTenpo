using Asp.Versioning.Builder;
using eTenpo.Basket.Api.EndpointFilters;
using eTenpo.Basket.Api.Models;
using eTenpo.Basket.Api.Services;
using eTenpo.Basket.Api.Validators;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace eTenpo.Basket.Api.Endpoints;

public static class BasketEndpoints
{
    public static WebApplication MapEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        var basketGroup = app.MapGroup("/api/v{version:apiVersion}/baskets")
            .WithOpenApi()
            .WithApiVersionSet(apiVersionSet);
        
        basketGroup.MapPost("/", CreateBasket)
            .WithName(nameof(CreateBasket))
            .AddEndpointFilter<ValidationFilter<BasketModel>>();

        basketGroup.MapGet("/{customerId:guid}", GetBasket)
            .WithName(nameof(GetBasket));
        
        basketGroup.MapPut("/{customerId:guid}", UpdateBasket)
            .WithName(nameof(UpdateBasket))
            .AddEndpointFilter<ValidationFilter<BasketModel>>();

        basketGroup.MapDelete("/{customerId:guid}", DeleteBasket)
            .WithName(nameof(DeleteBasket));
        
        return app;
    }

    public static async Task<CreatedAtRoute> CreateBasket(
        [FromBody] BasketModel basket,
        IBasketRepository basketRepository)
    {
        ArgumentNullException.ThrowIfNull(basketRepository);
        var resultBasket = await basketRepository.CreateBasket(basket);

        return TypedResults.CreatedAtRoute(nameof(GetBasket), new { customerId = resultBasket.CustomerId });
    }
    
    public static async Task<Results<Ok<BasketModel>, NotFound<ProblemDetails>>> GetBasket(
        [FromRoute, RequiredAndNotEmpty] Guid customerId,
        IBasketRepository basketRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        ArgumentNullException.ThrowIfNull(basketRepository);
        ArgumentNullException.ThrowIfNull(httpContextAccessor);
        
        var basket = await basketRepository.GetBasket(customerId);

        if (basket is null)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Type = "NotFoundException",
                Title = "The requested entity could not be found",
                Detail = $"The basket with customer id \"{customerId}\" could not be found",
                Instance = httpContextAccessor.HttpContext?.Request.Path
            };

            return TypedResults.NotFound(problemDetails);
        }

        return TypedResults.Ok(basket);
    }
    
    public static async Task<Results<Ok<BasketModel>, NotFound<ProblemDetails>>> UpdateBasket(
        [FromRoute, RequiredAndNotEmpty] Guid customerId,
        [FromBody] BasketModel basket,
        IBasketRepository basketRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        ArgumentNullException.ThrowIfNull(basketRepository);
        ArgumentNullException.ThrowIfNull(httpContextAccessor);
        
        var currentBasket = await basketRepository.GetBasket(customerId);

        if (currentBasket is null)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Type = "NotFoundException",
                Title = "The requested entity could not be found",
                Detail = $"The basket with customer id \"{customerId}\" could not be found",
                Instance = httpContextAccessor.HttpContext?.Request.Path
            };

            return TypedResults.NotFound(problemDetails);
        }
                    
        var updatedBasket = await basketRepository.UpdateBasket(basket);
        return TypedResults.Ok(updatedBasket);
    }

    public static async Task<Results<NoContent, NotFound<ProblemDetails>>> DeleteBasket(
        [FromRoute, RequiredAndNotEmpty] Guid customerId,
        IBasketRepository basketRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        ArgumentNullException.ThrowIfNull(basketRepository);
        ArgumentNullException.ThrowIfNull(httpContextAccessor);
        
        var basket = await basketRepository.GetBasket(customerId);

        if (basket is null)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Type = "NotFoundException",
                Title = "The requested entity could not be found",
                Detail = $"The basket with customer id \"{customerId}\" could not be found",
                Instance = httpContextAccessor.HttpContext?.Request.Path
            };

            return TypedResults.NotFound(problemDetails);
        }

        await basketRepository.DeleteBasket(customerId);
        return TypedResults.NoContent();
    }
}
