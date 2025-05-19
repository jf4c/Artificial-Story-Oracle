using ASO.Domain.Game.Entities;
using ASO.Domain.Game.ValueObjects;

namespace ASO.Infra.Database.Seeds;

public abstract class AncestrySeed : ISeed
{
    public static void Seed(AppDbContext context)
    {
        if (context.Ancestries.Any()) return;
        context.Ancestries.AddRange(GetAncestries());
        context.SaveChanges();
    }

    private static List<Ancestry> GetAncestries()
    {
        return [
            Ancestry.Create(
                "Humano",
                "Versáteis e ambiciosos, os humanos são adaptáveis e determinados.",
                AttributeModifiers.Create(1, 0, 0, 0, 0, 0),
                1.0f,
                9),

            Ancestry.Create(
                "Anão",
                "Os anões são robustos e disciplinados, conhecidos por sua habilidade em mineração e metalurgia.",
                AttributeModifiers.Create(0, 0, 2, 0, 1, -1),
                1.0f,
                6),

            Ancestry.Create(
                "Elfo",
                "Graciosos e longevos, os elfos têm afinidade com a natureza e magia.",
                AttributeModifiers.Create(0, 2, -1, 0, 1, 0),
                1.0f,
                9),

            Ancestry.Create(
                "Goblin",
                "Pequenos, astutos e ágeis, os goblins vivem à margem da sociedade.",
                AttributeModifiers.Create(0, 2, 0, 0, -1, 0),
                0.5f,
                6),

            Ancestry.Create(
                "Lefou",
                "Marcados pelo toque da Tormenta, os lefou possuem características aberrantes e poderes sombrios.",
                AttributeModifiers.Create(0, 0, 0, 1, 0, 1),
                1.0f,
                9),

            Ancestry.Create(
                "Minotauro",
                "Fortes e orgulhosos, os minotauros são guerreiros temidos e honrados.",
                AttributeModifiers.Create(2, 0, 1, -1, 0, 0),
                1.0f,
                9),

            Ancestry.Create(
                "Qareen",
                "Encantadores e misteriosos, os qareen possuem forte ligação com os planos elementais.",
                AttributeModifiers.Create(0, 0, 0, 0, -1, 2),
                1.0f,
                9),

            Ancestry.Create(
                "Dahllan",
                "Filhos da natureza, os dahllan possuem feições vegetais e profunda ligação com a vida selvagem.",
                AttributeModifiers.Create(1, 0, 0, 0, 2, -1),
                1.0f,
                9),

            Ancestry.Create(
                "Hynne",
                "Pequenos e alegres, os hynne são conhecidos por sua curiosidade e energia.",
                AttributeModifiers.Create(0, 1, 0, 0, 0, 1),
                0.5f,
                6),

            Ancestry.Create(
                "Kliren",
                "Brilhantes e inventivos, os kliren valorizam o conhecimento e a tecnologia.",
                AttributeModifiers.Create(0, 0, -1, 2, 1, 0),
                1.0f,
                9),

            Ancestry.Create(
                "Medusa",
                "Criaturas místicas com aparência petrificante, as medusas são tanto temidas quanto fascinantes.",
                AttributeModifiers.Create(0, 1, 0, 1, 0, 0),
                1.0f,
                9),

            Ancestry.Create(
                "Osteon",
                "Mortos-vivos conscientes, os osteons mantêm a mente lúcida mesmo após a morte.",
                AttributeModifiers.Create(0, 0, 1, 1, 0, -1),
                1.0f,
                9),

            Ancestry.Create(
                "Sulfure",
                "Nascidos do caos e da magia da Tormenta, os sulfure são ardilosos e intensos.",
                AttributeModifiers.Create(0, 1, 0, 1, -1, 0),
                1.0f,
                9),

            Ancestry.Create(
                "Trog",
                "Reptilianos rústicos e agressivos, os trogs vivem em pântanos e cavernas.",
                AttributeModifiers.Create(2, 0, 1, -1, 0, 0),
                1.0f,
                9)
        ];
    }
}