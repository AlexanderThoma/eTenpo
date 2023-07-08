namespace eTenpo.Product.Domain.Contracts;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}