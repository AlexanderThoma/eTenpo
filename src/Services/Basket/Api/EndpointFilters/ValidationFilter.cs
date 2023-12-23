using FluentValidation;

namespace eTenpo.Basket.Api.EndpointFilters;

public class ValidationFilter<T> : IEndpointFilter
    where T : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(next);
        
        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
        if (validator is null)
        {
            return await next(context);
        }
        
        var entity = context.Arguments
            .OfType<T>()
            .FirstOrDefault(a => a.GetType() == typeof(T));
        if (entity is null)
        {
            return Results.Problem($"Validator for give type \"{typeof(T)}\" does not exist");
        }
        
        var validation = await validator.ValidateAsync(entity);
        if (validation.IsValid)
        {
            return await next(context);
        }

        return Results.ValidationProblem(validation.ToDictionary());
    }
}

