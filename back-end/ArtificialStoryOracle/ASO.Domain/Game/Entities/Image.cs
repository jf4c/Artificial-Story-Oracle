using ASO.Domain.Shared.Entities;

namespace ASO.Domain.Game.Entities;

public class Image : Entity
{
    #region Constructors

    private Image()
    {
        Name = null!;
        Url = null!;
        Description = null;
    }

    private Image(string name, string url, string description)
    {
        Name = name;
        Url = url;
        Description = description;
    }

    #endregion

    public static Image Create(string name, string description)
    {
        var url = $"./assets/Character/{name}.png";
        return new Image(name, url, description);
    }

    public string Name { get; }
    public string Url { get; }
    public string? Description { get; }
}