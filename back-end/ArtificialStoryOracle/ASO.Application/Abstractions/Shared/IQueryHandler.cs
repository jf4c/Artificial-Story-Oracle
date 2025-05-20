namespace ASO.Application.Abstractions.Shared;

public interface IQueryHandler<in TRequest, TResponse>
    where TRequest : IQuery
    where TResponse : IResponse
{
    Task<TResponse> Handle(TRequest request);
}

public interface IQueryHandler<TResponse>
    where TResponse : IResponse
{
    Task<TResponse> Handle();
}