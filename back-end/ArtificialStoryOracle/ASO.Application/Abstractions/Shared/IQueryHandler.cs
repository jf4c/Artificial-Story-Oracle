using ASO.Application.Pagination;

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

public interface IQueryPaginatedHandler<in TFilter, TResponse>
    where TFilter : IQuery
    where TResponse : class, IResponse
{
    Task<PaginatedResult<TResponse>> Handle(TFilter filter);
}