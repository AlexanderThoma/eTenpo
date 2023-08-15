using eTenpo.Product.Domain.Exceptions.Base;

namespace eTenpo.Product.Domain.Exceptions;

public class EntityNotFoundException : NotFoundException
{
    public EntityNotFoundException()
    {
    }

    public EntityNotFoundException(string message)
        : base(message)
    {
    }

    public EntityNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}