using ASO.Domain.Game.Entities;

namespace ASO.Infra.Database.Seeds;

public class ImageSeed : ISeed
{
    public static void Seed(AppDbContext context)
    {
        if (context.Images.Any()) return;
        context.Images.AddRange(GetImages());
        context.SaveChanges();
    }

    private static List<Image> GetImages()
    {
        return
        [
            Image.Create("assassin1", "Elfa assassina ágil com manto verde e adaga."),
            Image.Create("assassin2", "Assassino humano com capuz e lâmina afiada."),
            Image.Create("bard", "Barda humana sorridente tocando alaúde."),
            Image.Create("bard2", "Bardo de pele verde cantando melodias antigas."),
            Image.Create("mage1", "Mago élfico com chapéu estrelado e cajado mágico."),
            Image.Create("mage2", "Maga nobre com manto azul e bastão dourado."),
            Image.Create("mage3", "Mago de túnica vermelha com poder elemental."),
            Image.Create("mage4", "Maga sábia com cabelo branco e pergaminho."),
            Image.Create("mage5", "Feiticeira jovem de manto azul e chapéu largo."),
            Image.Create("mage6", "Maga focada com cristal rosa e capa verde."),
            Image.Create("monk1", "Monge careca com túnica laranja e postura serena."),
            Image.Create("orch", "Orc guerreiro com armadura pesada e maça."),
            Image.Create("orch1", "Orc ágil com espada, pronto para atacar."),
            Image.Create("priest1", "Sacerdote com barba segurando uma chama divina."),
            Image.Create("priest2", "Sacerdotisa jovem conjurando fogo."),
            Image.Create("rogue1", "Ladina encapuzada com olhar atento."),
            Image.Create("unknown", "Figura misteriosa com rosto oculto."),
            Image.Create("warrior1", "Guerreiro humano com espada e armadura."),
            Image.Create("warrior2", "Orc guerreira com machado, pronta para a guerra."),
            Image.Create("warrior3", "Cavaleiro experiente com escudo e armadura azul."),
            Image.Create("warrior4", "Veterano careca e barbudo com armadura robusta.")
        ];
    }
}