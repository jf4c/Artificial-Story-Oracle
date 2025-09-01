namespace ASO.Application.Pagination;

public record PaginatedQueryBase
{
    public int Page { get; init; } = 1;
    
    public int PageSize { get; init; } = 10;
}