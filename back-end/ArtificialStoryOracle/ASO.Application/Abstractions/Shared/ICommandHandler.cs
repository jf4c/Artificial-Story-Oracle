namespace ASO.Application.Abstractions.Shared;

public interface ICommandHandler <in TRequest, out TResponse>
    where TRequest : ICommand
    where TResponse : IResponse
{
    TResponse Handle(TRequest command);
}