namespace eTenpo.Product.Domain.Exceptions.Base;

public abstract class ProductDomainException : Exception
{
    public ProductDomainException()
    {
    }

    public ProductDomainException(string message)
        : base(message)
    {
    }

    public ProductDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}