using eTenpo.Product.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace eTenpo.Product.Api.Middleware;

public class GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger, IWebHostEnvironment environment)
    : IMiddleware
{
    private readonly ILogger<GlobalExceptionMiddleware> logger = logger ?? throw new ArgumentNullException(nameof(logger));

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
        finally
        {
            // make sure that the log is really written to sink
            await Serilog.Log.CloseAndFlushAsync();
        }
    }

    private async Task HandleException(HttpContext context, Exception exception)
    {
        var problemDetails = CreateProblemDetails(context.Request.Path, exception);

        if (environment.IsDevelopment())
        {
            problemDetails.Extensions.Add("stackTrace", exception);
        }
        
        context.Response.ContentType = "application/json";
        
        // set correct statuscode for frontend in response (would be 200 otherwise)
        context.Response.StatusCode = problemDetails.Status!.Value;
        
        await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
    }

    private ProblemDetails CreateProblemDetails(PathString requestPath, Exception exception)
    {
        var problemDetails = exception switch
        {
            ValidationException ex => new ValidationProblemDetails(ex.Errors.GroupBy(x => x.PropertyName)
                .ToDictionary(x => x.Key, failures => failures.Select(y => y.ErrorMessage).ToArray()))
            {
                Title = "One or more validation errors occurred in the pipeline",
                Status = StatusCodes.Status400BadRequest,
                Type = nameof(ValidationException),
                Detail = ex.Message,
                Instance = requestPath
            },
            ProductValidationException ex => new ProblemDetails
            {
                Status = StatusCodes.Status422UnprocessableEntity,
                Type = nameof(ProductValidationException),
                Title = "An validation error occurred in the product domain",
                Detail = ex.Message,
                Instance = requestPath
            },
            EntityNotFoundException ex => new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Type = nameof(EntityNotFoundException),
                Title = "The requested entity could not be found",
                Detail = ex.Message,
                Instance = requestPath
            },
            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = "Server error",
                Title = "An error occurred while processing your request",
                Detail = "An internal server error has occurred. See logs for more details",
                Instance = requestPath
            }
        };
        
        return problemDetails;
    }
}