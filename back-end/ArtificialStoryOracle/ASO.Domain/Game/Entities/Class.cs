using ASO.Domain.Shared.Entities;

namespace ASO.Domain.Game.Entities;

public class Class 
{
    private Class(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public static Class Create(string name, string description)
    {
        return new Class(name, description);
    }
    
    public string Name { get; }
    public string Description { get; }
}