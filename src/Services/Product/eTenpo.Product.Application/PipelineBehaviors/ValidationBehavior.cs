using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace eTenpo.Product.Application.PipelineBehaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => this.validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        List<ValidationFailure> failures = new ();

        foreach (var validator in this.validators)
        {
            var result = await validator.ValidateAsync(context, cancellationToken);
            
            failures.AddRange(result.Errors.Where(validationFailure => validationFailure is not null).Distinct());
        }
        
        if (failures.Any())
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
}