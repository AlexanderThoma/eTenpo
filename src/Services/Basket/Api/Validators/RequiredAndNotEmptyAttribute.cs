using System.ComponentModel.DataAnnotations;

namespace eTenpo.Basket.Api.Validators;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class RequiredAndNotEmptyAttribute() : ValidationAttribute
{
    private const string NotEmptyErrorMessage = "The {0} field must not be empty";
    private const string RequiredErrorMessage = "The {0} field is required";

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return new ValidationResult(NotEmptyErrorMessage);
        }
        
        if (value is not Guid test)
        {
            return new ValidationResult(RequiredErrorMessage);
        }

        return test == Guid.Empty ? new ValidationResult(NotEmptyErrorMessage) : ValidationResult.Success;
    }
}

