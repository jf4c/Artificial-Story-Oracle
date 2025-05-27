using ASO.Domain.Shared.ValueObjects;

namespace ASO.Domain.Game.ValueObjects;

public record Statistics : ValueObject
{
    #region  constuctors

    private Statistics(int initManaPoints, int initHealthPoints)
    {
        InitManaPoints = initManaPoints;
        InitHealthPoints = initHealthPoints;
    }

    #endregion
    
    #region Factory Methods
    
    public static Statistics Create(int mp, int hp) =>
        new(mp, hp);

    #endregion
    
    #region Properties
    
    public int InitManaPoints { get; set; }
    public int InitHealthPoints { get; set; }
    
    #endregion
}