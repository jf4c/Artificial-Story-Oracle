using ASO.Domain.Shared.Entities;
using ASO.Domain.Shared.ValueObjects;
using ASO.Domain.ValueObjects;

namespace ASO.Domain.Game.Entities;

public class Character : Entity
{
    public Character(Name name, Statistics statistics) : base(Guid.NewGuid())
    {
        Name = name;
        Statistics = statistics;
    }
    
    public Name Name { get; }
    public Statistics Statistics { get; }
    
} 