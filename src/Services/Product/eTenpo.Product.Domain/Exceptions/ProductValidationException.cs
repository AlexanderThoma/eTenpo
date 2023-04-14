using eTenpo.Product.Domain.Exceptions.Base;

namespace eTenpo.Product.Domain.Exceptions;

public class ProductValidationException : ProductDomainException
{
    public ProductValidationException()
    {
    }

    public ProductValidationException(string message)
        : base(message)
    {
    }

    public ProductValidationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}