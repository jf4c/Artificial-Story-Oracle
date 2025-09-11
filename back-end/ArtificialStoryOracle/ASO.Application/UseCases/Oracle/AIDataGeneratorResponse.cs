using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Oracle;

public record AIDataGeneratorResponse : IResponse
{
    public string Text { get; set; } = string.Empty;
}
