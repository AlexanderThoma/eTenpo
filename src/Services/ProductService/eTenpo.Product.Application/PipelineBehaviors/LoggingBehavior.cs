using MediatR;
using Microsoft.Extensions.Logging;

namespace eTenpo.Product.Application.PipelineBehaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        this.logger = logger;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Handling request {@RequestName}, {@DateTimeUtc}", 
            typeof(TRequest).Name,
            DateTime.UtcNow);

        var result = await next();

        this.logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", 
            typeof(TRequest).Name,
            DateTime.UtcNow);
        
        return result;
    }
}