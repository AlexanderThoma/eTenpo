using MediatR;

namespace eTenpo.Product.Application.CommandQueryAbstractions;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}