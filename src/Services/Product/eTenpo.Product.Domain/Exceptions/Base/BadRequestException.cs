namespace eTenpo.Product.Domain.Exceptions.Base;

public class BadRequestException : ProductDomainException
{
    public BadRequestException()
    {
    }

    public BadRequestException(string message)
        : base(message)
    {
    }

    public BadRequestException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}