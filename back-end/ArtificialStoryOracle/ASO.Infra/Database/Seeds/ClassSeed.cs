using ASO.Domain.Game.Entities;
using ASO.Domain.Game.Enums;


namespace ASO.Infra.Database.Seeds;

public class ClassSeed : ISeed
{
    public static void Seed(AppDbContext context)
    {
        if (context.Classes.Any()) return;
        
        context.Classes.AddRange(GetClass());
        context.SaveChanges();
    }
    
    private static List<Class> GetClass()
    {
        
        return
        [
            Class.Create(
                "Arcanista",
                "Um mestre das artes arcanas, capaz de conjurar poderosos feitiços.",
                50,
                100),


            Class.Create(
                "Bárbaro",
                "Um guerreiro selvagem que luta com fúria primitiva.",
                80,
                20),


            Class.Create(
                "Bardo",
                "Um artista aventureiro que usa música e magia para inspirar aliados.",
                60,
                60),


            Class.Create(
                "Bucaneiro",
                "Pirata destemido, mestre dos mares e dos disparos rápidos.",
                70,
                30),


            Class.Create(
                "Cavaleiro",
                "Um guerreiro nobre, símbolo de honra e bravura.",
                80,
                20),


            Class.Create(
                "Clérigo",
                "Um servo divino que canaliza o poder dos deuses.",
                60,
                60),


            Class.Create(
                "Druida",
                "Guardião da natureza, com poderes ligados à terra.",
                60,
                60),


            Class.Create(
                "Guerreiro",
                "Combatente versátil, mestre em armas e estratégias de batalha.",
                80,
                20),


            Class.Create(
                "Inventor",
                "Gênio das engenhocas, usando ciência e engenhosidade.",
                50,
                100),


            Class.Create(
                "Ladino",
                "Trapaceiro furtivo, mestre em enganar e se esconder.",
                60,
                40),


            Class.Create(
                "Nobre",
                "Líder por natureza, influente e refinado.",
                60,
                40),


            Class.Create(
                "Paladino",
                "Campeão da justiça, guiado pela fé e honra.",
                70,
                30)
        ];
    }
}