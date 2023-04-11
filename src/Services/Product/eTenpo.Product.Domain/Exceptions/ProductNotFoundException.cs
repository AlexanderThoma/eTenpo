using eTenpo.Product.Domain.Exceptions.Base;

namespace eTenpo.Product.Domain.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException()
    {
    }

    public ProductNotFoundException(string message)
        : base(message)
    {
    }

    public ProductNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}