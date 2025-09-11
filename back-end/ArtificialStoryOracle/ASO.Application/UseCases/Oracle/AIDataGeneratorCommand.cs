using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Oracle;

public record AIDataGeneratorCommand : ICommand
{
    public string Name { get; set; } = string.Empty;
    public string Ancestry { get; set; } = string.Empty;
    public string Class { get; set; } = string.Empty;
    public string Supplements { get; set; } = string.Empty;
}