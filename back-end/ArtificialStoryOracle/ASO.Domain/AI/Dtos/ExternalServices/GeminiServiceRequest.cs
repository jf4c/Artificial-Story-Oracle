namespace ASO.Domain.AI.Dtos.ExternalServices;

public record GeminiServiceRequest(List<ContentDto> Contents);

public record ContentDto(List<Part> Parts)
{
    public static ContentDto Generate(
        string name, 
        string ancestry, 
        string @class, 
        string supplements)
    {
        var content = Part.GenerateUserPrompt(name, ancestry, @class, supplements);
        return new ContentDto(new List<Part> { content });
    }
}

