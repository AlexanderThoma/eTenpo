namespace eTenpo.Product.Domain.Exceptions.Base;

public abstract class NotFoundException : ProductDomainException
{
    protected NotFoundException()
    {
    }

    protected NotFoundException(string message)
        : base(message)
    {
    }

    protected NotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}