namespace eTenpo.Product.Domain.Exceptions.Base;

public abstract class ProductDomainException : Exception
{
    protected ProductDomainException()
    {
    }

    protected ProductDomainException(string message)
        : base(message)
    {
    }

    protected ProductDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}