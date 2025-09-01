using ASO.Domain.Shared.Entities;

namespace ASO.Application.Builders;

public abstract class QueryBuilderBase<TBuilder, TEntity, TFilter> where TEntity : Entity
{
    protected static TBuilder _instance { get; set; } = default!;
    protected IQueryable<TEntity> Query { get; set; } = null!;
    protected TFilter Filter { get; set; } = default!;
    public bool IsOrdered { get; set; } = false;

    public void SetIsOrdered()
    {
        IsOrdered = true;
    }

    public virtual IQueryable<TEntity> BuildQuery()
    {
        return Query;
    }

    public virtual TBuilder SetOrderBy()
    {
        if (IsOrdered is false)
        {
            Query = Query.OrderByDescending(i => i.Tracker.UpdatedAtUtc)
                .ThenBy(x => x.Id);

            SetIsOrdered();
        }

        return _instance;
    }
    
}