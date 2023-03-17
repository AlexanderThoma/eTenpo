using System.Net;
using eTenpo.Product.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eTenpo.Product.Api.Middleware;

public class GlobalExceptionMiddleware : IMiddleware
{
    private readonly ILogger logger;

    public GlobalExceptionMiddleware(ILogger logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private async Task HandleException(HttpContext context, Exception exception)
    {
        var problemDetails = new ProblemDetails();
        var response = context.Response;
        context.Response.ContentType = "application/json";

        switch (exception)
        {
            case ProductDomainException:
                break;
            // case validationException break;
            default:
                problemDetails = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Server error",
                    Title = "Server error",
                    Detail = "An internal server error has occurred"
                };
                break;
        }

        string json = JsonConvert.SerializeObject(problemDetails);
            

        // TODO: create response code and message according to exception type
    }
}