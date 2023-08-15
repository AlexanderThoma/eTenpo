namespace eTenpo.Product.Domain.Exceptions.Base;

public abstract class CategoryDomainException : Exception
{
    protected CategoryDomainException()
    {
    }

    protected CategoryDomainException(string message)
        : base(message)
    {
    }

    protected CategoryDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}