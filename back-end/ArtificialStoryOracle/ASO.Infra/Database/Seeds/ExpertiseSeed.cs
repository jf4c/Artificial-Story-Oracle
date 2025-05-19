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
            Expertise.Create("Acrobacia", AttributeBase.Dexterity, trained: false, armorPenalty: true),
            Expertise.Create("Adestramento", AttributeBase.Charisma, trained: true),
            Expertise.Create("Atletismo", AttributeBase.Strength, trained: false, armorPenalty: true),
            Expertise.Create("Atuação", AttributeBase.Charisma),
            Expertise.Create("Cavalgar", AttributeBase.Dexterity, trained: false, armorPenalty: true),
            Expertise.Create("Conhecimento", AttributeBase.Intelligence, trained: true),
            Expertise.Create("Cura", AttributeBase.Wisdom),
            Expertise.Create("Diplomacia", AttributeBase.Charisma),
            Expertise.Create("Enganação", AttributeBase.Charisma),
            Expertise.Create("Furtividade", AttributeBase.Dexterity, trained: false, armorPenalty: true),
            Expertise.Create("Guerra", AttributeBase.Intelligence, trained: true),
            Expertise.Create("Iniciativa", AttributeBase.Dexterity),
            Expertise.Create("Intimidação", AttributeBase.Charisma),
            Expertise.Create("Intuição", AttributeBase.Wisdom),
            Expertise.Create("Investigação", AttributeBase.Intelligence),
            Expertise.Create("Jogatina", AttributeBase.Charisma),
            Expertise.Create("Luta", AttributeBase.Strength, trained: false, armorPenalty: true),
            Expertise.Create("Misticismo", AttributeBase.Intelligence, trained: true),
            Expertise.Create("Nobreza", AttributeBase.Intelligence, trained: true),
            Expertise.Create("Ofício", AttributeBase.Intelligence),
            Expertise.Create("Percepção", AttributeBase.Wisdom),
            Expertise.Create("Pilotagem", AttributeBase.Dexterity, trained: true, armorPenalty: true),
            Expertise.Create("Pontaria", AttributeBase.Dexterity, trained: false, armorPenalty: true),
            Expertise.Create("Reflexos", AttributeBase.Dexterity),
            Expertise.Create("Religião", AttributeBase.Wisdom, trained: true),
            Expertise.Create("Sobrevivência", AttributeBase.Wisdom),
            Expertise.Create("Vontade", AttributeBase.Wisdom)
        ];
    }
}