using ASO.Domain.Shared.Aggregates.Abstractions;
using ASO.Domain.Shared.Entities;
using ASO.Domain.Shared.ValueObjects;

namespace ASO.Domain.Game.Entities;

public class Narrative : Entity, IAggragateRoot
{
    #region Constructors
    
    private Narrative()
    {
        
    }
    
    #endregion
    
    public string Titule { get; } = string.Empty;
    public string Description { get; } = string.Empty;
}