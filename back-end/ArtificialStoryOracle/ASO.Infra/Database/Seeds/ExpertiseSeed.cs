using ASO.Domain.Game.Entities;
using ASO.Domain.Game.Enums;


namespace ASO.Infra.Database.Seeds;

public abstract class ExpertiseSeed : ISeed
{
    public static void Seed(AppDbContext context)
    {
        if (context.Expertises.Any()) return;
        context.Expertises.AddRange(GetExpertises());
        context.SaveChanges();
    }

    private static List<Expertise> GetExpertises()
    {
        return
        [
            Expertise.Create(ExpertiseName.Acrobatics, AttributeBase.Dexterity, trained: false, armorPenalty: true),
            Expertise.Create(ExpertiseName.Acrobatics, AttributeBase.Dexterity, trained: false, armorPenalty: true),
            Expertise.Create(ExpertiseName.AnimalHandling, AttributeBase.Charisma, trained: true),
            Expertise.Create(ExpertiseName.Athletics, AttributeBase.Strength, trained: false, armorPenalty: true),
            Expertise.Create(ExpertiseName.Performance, AttributeBase.Charisma),
            Expertise.Create(ExpertiseName.Ride, AttributeBase.Dexterity, trained: false, armorPenalty: true),
            Expertise.Create(ExpertiseName.Knowledge, AttributeBase.Intelligence, trained: true),
            Expertise.Create(ExpertiseName.Healing, AttributeBase.Wisdom),
            Expertise.Create(ExpertiseName.Diplomacy, AttributeBase.Charisma),
            Expertise.Create(ExpertiseName.Deception, AttributeBase.Charisma),
            Expertise.Create(ExpertiseName.Stealth, AttributeBase.Dexterity, trained: false, armorPenalty: true),
            Expertise.Create(ExpertiseName.War, AttributeBase.Intelligence, trained: true),
            Expertise.Create(ExpertiseName.Initiative, AttributeBase.Dexterity),
            Expertise.Create(ExpertiseName.Intimidation, AttributeBase.Charisma),
            Expertise.Create(ExpertiseName.Insight, AttributeBase.Wisdom),
            Expertise.Create(ExpertiseName.Investigation, AttributeBase.Intelligence),
            Expertise.Create(ExpertiseName.Gambling, AttributeBase.Charisma),
            Expertise.Create(ExpertiseName.Fighting, AttributeBase.Strength, trained: false, armorPenalty: true),
            Expertise.Create(ExpertiseName.Mysticism, AttributeBase.Intelligence, trained: true),
            Expertise.Create(ExpertiseName.Nobility, AttributeBase.Intelligence, trained: true),
            Expertise.Create(ExpertiseName.Crafting, AttributeBase.Intelligence),
            Expertise.Create(ExpertiseName.Perception, AttributeBase.Wisdom),
            Expertise.Create(ExpertiseName.Piloting, AttributeBase.Dexterity, trained: true, armorPenalty: true),
            Expertise.Create(ExpertiseName.Shooting, AttributeBase.Dexterity, trained: false, armorPenalty: true),
            Expertise.Create(ExpertiseName.Reflexes, AttributeBase.Dexterity),
            Expertise.Create(ExpertiseName.Religion, AttributeBase.Wisdom, trained: true),
            Expertise.Create(ExpertiseName.Survival, AttributeBase.Wisdom),
            Expertise.Create(ExpertiseName.Willpower, AttributeBase.Wisdom)
        ];
    }
}