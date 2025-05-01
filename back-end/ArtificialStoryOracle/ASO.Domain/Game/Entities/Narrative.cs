using ASO.Domain.Shared.Entities;
using ASO.Domain.Shared.ValueObjects;
using ASO.Domain.ValueObjects;

namespace ASO.Domain.Game.Entities;

public class Narrative : Entity
{
    public Narrative() : base(Guid.NewGuid())
    {
        
    }
    
    public string Titule { get; } = string.Empty;
    public string Description { get; } = string.Empty;
}