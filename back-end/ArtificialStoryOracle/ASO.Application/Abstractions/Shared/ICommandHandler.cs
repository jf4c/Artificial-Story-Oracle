﻿namespace ASO.Application.Abstractions.Shared;

public interface ICommandHandler <in TRequest, out TResponse>
    where TRequest : ICommand
    where TResponse : IResponse
{
    TResponse Handle(TRequest command);
}

public interface ICommandHandlerAsync <in TRequest, TResponse>
    where TRequest : ICommand
    where TResponse : IResponse
{
    Task<TResponse> HandleAsync(TRequest command);
}

public interface ICommandHandlerAsync<in TRequest>
    where TRequest : ICommand
{
    Task HandleAsync(TRequest command);
}
