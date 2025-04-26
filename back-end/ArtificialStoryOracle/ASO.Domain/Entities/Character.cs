namespace ASO.Domain.Entities;

public class Character : Entity
{
    public Character() : base(Guid.NewGuid())
    {
        
    }
    
    public string Name { get; } = string.Empty;
}