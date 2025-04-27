namespace ASO.Domain.ValueObjects;

public record Statistics : ValueObject
{
    #region  constuctors

    private Statistics(int skillPoints, int hitPoints)
    {
        SkillPoints = skillPoints;
        HitPoints = hitPoints;
        Level = 0;
    }

    #endregion
    
    #region Factory Methods
    
    public static Statistics Create(int mp, int hp, int level) =>
        new(mp, hp);

    #endregion
    
    #region Properties
    
    public int SkillPoints { get; set; }
    public int HitPoints { get; set; }
    public int Level { get; set; }
    
    #endregion
    
    #region Methods
    
    public void LevelUp()
    {
        Level++;
        SkillPoints += 10;
        HitPoints += 10;
    }
    
    public void LevelDown()
    {
        if (Level > 1)
        {
            Level--;
            SkillPoints -= 10;
            HitPoints -= 10;
        }
    }
    
    #endregion
}