using System.Linq.Expressions;
using ASO.Application.Enums;
using ASO.Application.Pagination;
using Microsoft.EntityFrameworkCore;

namespace ASO.Application.Extensions;

public static class QueryableExtensions
{
    public static async Task<PaginatedResult<T>> GetPaginatedAsync<T>(
        this IQueryable<T> query,
        int pageIndex,
        int pageSize,
        CancellationToken ct = default) where T : class
    {
        var result = new PaginatedResult<T>
        {
            CurrentPage = pageIndex,
            PageSize = pageSize,
            RowCount = await query.CountAsync(cancellationToken: ct)
        };

        var pageCount = (double)result.RowCount / pageSize;
        result.PageCount = (int)Math.Ceiling(pageCount);
        var skip = (pageIndex - 1) * pageSize;

        result.Results = await query
            .AsNoTracking()
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(ct);

        return result;
    }

    public static IOrderedQueryable<T> OrderByAscDesc<T, TKey>(
        this IQueryable<T> queryable,
        Expression<Func<T, TKey>> expression,
        string orderByType = OrderType.Asc)
    {
        if (orderByType.Equals(OrderType.Asc, StringComparison.InvariantCultureIgnoreCase))
            return queryable.OrderBy(expression);

        return queryable.OrderByDescending(expression);
    }
}