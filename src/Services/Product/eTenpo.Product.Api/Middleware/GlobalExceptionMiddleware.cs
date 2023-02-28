namespace eTenpo.Product.Api.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger logger)
    {
        this.next = next ?? throw new ArgumentNullException(nameof(next));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        // TODO: catch (DomainException) { }
        catch (Exception ex)
        {
            // TODO: create response code and message according to exception type
        }
    }
}