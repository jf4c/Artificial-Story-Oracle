using ASO.Domain.ValueObjects;

namespace ASO.Domain.Entities;

public class Character : Entity
{
    public Character(Name name, Statistics statistics) : base(Guid.NewGuid(), Tracker.Create())
    {
        Name = name;
        Statistics = statistics;
    }
    
    public Name Name { get; }
    public Statistics Statistics { get; }
    
} 