using MediatR;

namespace eTenpo.Product.Application.CommandQueryAbstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}