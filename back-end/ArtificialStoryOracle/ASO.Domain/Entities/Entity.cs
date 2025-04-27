using ASO.Domain.ValueObjects;

namespace ASO.Domain.Entities;

public abstract class Entity(Guid id, Tracker tracker) : IEquatable<Guid>
{
    #region Properties
    public Guid Id { get; } = id;
    public Tracker Tracker { get; } = tracker;
    
    #endregion

    #region Equatable Implementation

    public bool Equals(Guid id) => Id == id;
    
    public override int GetHashCode() => Id.GetHashCode();

    #endregion
}