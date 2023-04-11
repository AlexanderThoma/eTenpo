using eTenpo.Product.Domain.Exceptions.Base;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eTenpo.Product.Api.Middleware;

public class GlobalExceptionMiddleware : IMiddleware
{
    private readonly ILogger logger;
    private readonly IWebHostEnvironment environment;

    public GlobalExceptionMiddleware(ILogger logger, IWebHostEnvironment environment)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.environment = environment;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error occurred");
            await HandleException(context, ex);
        }
    }

    private async Task HandleException(HttpContext context, Exception exception)
    {
        var response = context.Response;
        context.Response.ContentType = "application/json";

        var problemDetails = CreateProblemDetails(context, exception);

        if (environment.IsDevelopment())
        {
            problemDetails.Extensions.Add("stackTrace", exception);
        }

        var json = JsonConvert.SerializeObject(problemDetails);

        await response.WriteAsync(json);
    }

    private ProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
    {
        var problemDetails = exception switch
        {
            ValidationException ex => new ValidationProblemDetails(ex.Errors.GroupBy(x => x.PropertyName)
                .ToDictionary(x => x.Key, failures => failures.Select(y => y.ErrorMessage).ToArray()))
            {
                Title = "One or more validation errors occurred",
                Status = StatusCodes.Status400BadRequest,
                Type = nameof(ValidationException),
                Detail = ex.Message,
                Instance = context.Request.Path
            },
            ProductDomainException ex => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = nameof(ProductDomainException),
                Title = "An error occurred in the product domain",
                Detail = ex.Message,
                Instance = context.Request.Path
            },
            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = "Server error",
                Title = "An error occurred while processing your request",
                Detail = "An internal server error has occurred. See logs for more details",
                Instance = context.Request.Path
            }
        };
        return problemDetails;
    }
}