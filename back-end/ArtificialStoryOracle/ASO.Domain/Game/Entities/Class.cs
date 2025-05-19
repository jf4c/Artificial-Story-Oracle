using ASO.Domain.Shared.Entities;

namespace ASO.Domain.Game.Entities;

public class Class : Entity
{
    
    #region Constructors
    
    private Class()
    {
        Name = null!;
        Description = null!;
        Characters = new();
    }
    
    private Class(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    #endregion
    
    public static Class Create(string name, string description)
    {
        return new Class(name, description);
    }
    
    public string Name { get; }
    public string Description { get; }
    public List<Character>? Characters { get; }
}