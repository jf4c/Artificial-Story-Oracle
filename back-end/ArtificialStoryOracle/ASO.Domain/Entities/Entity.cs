namespace ASO.Domain.Entities;

public abstract class Entity(Guid id) : IEquatable<Guid>
{
    #region Properties
    public Guid Id { get; } = id;
    
    #endregion

    #region Equatable Implementation

    public bool Equals(Guid id) => Id == id;
    
    public override int GetHashCode() => Id.GetHashCode();

    #endregion
}