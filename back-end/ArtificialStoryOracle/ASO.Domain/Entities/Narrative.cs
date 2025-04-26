namespace ASO.Domain.Entities;

public class Narrative : Entity
{
    public Narrative() : base(Guid.NewGuid())
    {
        
    }
    
    public string Titule { get; } = string.Empty;
}