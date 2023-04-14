using eTenpo.Product.Domain.Common;

namespace eTenpo.Product.Domain.Contracts;

// TODO: enforcement of using IRepository<T> for each repository
// TODO: way to do it -> write unit test which gets all repositories via reflection and checks the implemented interfaces, use tests in pipeline

public interface IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
{
}