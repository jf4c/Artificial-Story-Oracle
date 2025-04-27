using ASO.Domain.ValueObjects;

namespace ASO.Domain.Entities;

public class Narrative : Entity
{
    public Narrative() : base(Guid.NewGuid(), Tracker.Create())
    {
        
    }
    
    public string Titule { get; } = string.Empty;
    public string Description { get; } = string.Empty;
}