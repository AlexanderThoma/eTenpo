using MediatR;

namespace eTenpo.Product.Application.PipelineBehaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    public LoggingBehavior()
    {
        
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // todo: log stuff

        var result = await next();
        
        // todo: log stuff again

        return result;
    }
}