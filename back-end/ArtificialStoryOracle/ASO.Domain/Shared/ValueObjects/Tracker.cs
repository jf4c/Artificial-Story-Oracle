using ASO.Domain.ValueObjects;

namespace ASO.Domain.Shared.ValueObjects;

public sealed record Tracker : ValueObject
{
    #region constructors
    
    private Tracker(DateTime createdAtUtc, DateTime updatedAtUtc)
    {
        CreatedAtUtc = createdAtUtc;
        UpdatedAtUtc = updatedAtUtc;
    }
    
    #endregion
    
    #region Factory Methods
    
    public static Tracker Create(DateTime createdAtUtc, DateTime updatedAtUtc) 
        => new(createdAtUtc, updatedAtUtc);
    
    public static Tracker Create() 
        => new(DateTime.UtcNow, DateTime.UtcNow);
    
    #endregion
    
    #region properties
    
    public DateTime CreatedAtUtc { get; }
    public DateTime UpdatedAtUtc { get; }
    
    #endregion
}