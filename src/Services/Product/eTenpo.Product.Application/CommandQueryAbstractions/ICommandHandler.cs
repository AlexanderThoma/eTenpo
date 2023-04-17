﻿using MediatR;

namespace eTenpo.Product.Application.CommandQueryAbstractions;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
}