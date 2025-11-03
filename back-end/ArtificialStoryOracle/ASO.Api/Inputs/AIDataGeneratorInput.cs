namespace ASO.Api.Inputs;

public record AIDataGeneratorInput
{
    public string Name { get; set; } = string.Empty;
    public string Ancestry { get; set; } = string.Empty;
    public string Class { get; set; } = string.Empty;
    public string Attributes { get; set; } = string.Empty;
    public string Skills { get; set; } = string.Empty;
    public string Supplements { get; set; } = string.Empty;
}