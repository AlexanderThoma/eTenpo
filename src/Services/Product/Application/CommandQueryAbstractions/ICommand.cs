using MediatR;

namespace eTenpo.Product.Application.CommandQueryAbstractions;

public interface ICommand<out TResponse> : IRequest<TResponse>;

public interface ICommand : IRequest;