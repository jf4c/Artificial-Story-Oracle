using ASO.Domain.Game.ValueObjects;
using ASO.Domain.Shared.Entities;

namespace ASO.Domain.Game.Entities;

public class Ancestry : Entity
{
    #region Constructors

    private Ancestry(string name, string backstory, float size, int displacement)
    {
        Name = name;
        Backstory = backstory;
        Size = size;
        Displacement = displacement;
    }

    #endregion

    public static Ancestry Create(string name, string backstory, float size, int displacement)
    {
        return new Ancestry(name, backstory, size, displacement);
    }

    public string Name { get; }
    public string Backstory { get; }
    public float Size { get; }
    public int Displacement { get; }
}