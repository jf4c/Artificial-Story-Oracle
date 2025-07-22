using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Images.GetAll;

public record GetAllImagesResponse : IResponse
{
    public IEnumerable<ImageDto> Images { get; init; } = new List<ImageDto>();
}

public record ImageDto
{
    public Guid Id { get; init; } = Guid.Empty;
    public string Name { get; init; } = string.Empty;
    public string Url { get; init; } = string.Empty;
    public string? Description { get; init; }
}