using eTenpo.Product.Domain.Exceptions.Base;

namespace eTenpo.Product.Domain.Exceptions;

public class CategoryValidationException : CategoryDomainException
{
    public CategoryValidationException()
    {
    }

    public CategoryValidationException(string message)
        : base(message)
    {
    }

    public CategoryValidationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}