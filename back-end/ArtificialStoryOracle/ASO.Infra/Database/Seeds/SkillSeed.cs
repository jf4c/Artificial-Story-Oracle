using ASO.Domain.Game.Entities;
using ASO.Domain.Game.Enums;


namespace ASO.Infra.Database.Seeds;

public abstract class SkillSeed : ISeed
{
    public static void Seed(AppDbContext context)
    {
        if (context.Skill.Any()) return;
        context.Skill.AddRange(GetSkills());
        context.SaveChanges();
    }

    private static List<Skill> GetSkills()
    {
        return
        [
            Skill.Create(SkillName.Acrobatics, AttributeBase.Dexterity, trained: false, armorPenalty: true),
            Skill.Create(SkillName.Acrobatics, AttributeBase.Dexterity, trained: false, armorPenalty: true),
            Skill.Create(SkillName.AnimalHandling, AttributeBase.Charisma, trained: true),
            Skill.Create(SkillName.Athletics, AttributeBase.Strength, trained: false, armorPenalty: true),
            Skill.Create(SkillName.Performance, AttributeBase.Charisma),
            Skill.Create(SkillName.Ride, AttributeBase.Dexterity, trained: false, armorPenalty: true),
            Skill.Create(SkillName.Knowledge, AttributeBase.Intelligence, trained: true),
            Skill.Create(SkillName.Healing, AttributeBase.Wisdom),
            Skill.Create(SkillName.Diplomacy, AttributeBase.Charisma),
            Skill.Create(SkillName.Deception, AttributeBase.Charisma),
            Skill.Create(SkillName.Stealth, AttributeBase.Dexterity, trained: false, armorPenalty: true),
            Skill.Create(SkillName.War, AttributeBase.Intelligence, trained: true),
            Skill.Create(SkillName.Initiative, AttributeBase.Dexterity),
            Skill.Create(SkillName.Intimidation, AttributeBase.Charisma),
            Skill.Create(SkillName.Insight, AttributeBase.Wisdom),
            Skill.Create(SkillName.Investigation, AttributeBase.Intelligence),
            Skill.Create(SkillName.Gambling, AttributeBase.Charisma),
            Skill.Create(SkillName.Fighting, AttributeBase.Strength, trained: false, armorPenalty: true),
            Skill.Create(SkillName.Mysticism, AttributeBase.Intelligence, trained: true),
            Skill.Create(SkillName.Nobility, AttributeBase.Intelligence, trained: true),
            Skill.Create(SkillName.Crafting, AttributeBase.Intelligence),
            Skill.Create(SkillName.Perception, AttributeBase.Wisdom),
            Skill.Create(SkillName.Piloting, AttributeBase.Dexterity, trained: true, armorPenalty: true),
            Skill.Create(SkillName.Shooting, AttributeBase.Dexterity, trained: false, armorPenalty: true),
            Skill.Create(SkillName.Reflexes, AttributeBase.Dexterity),
            Skill.Create(SkillName.Religion, AttributeBase.Wisdom, trained: true),
            Skill.Create(SkillName.Survival, AttributeBase.Wisdom),
            Skill.Create(SkillName.Willpower, AttributeBase.Wisdom)
        ];
    }
}