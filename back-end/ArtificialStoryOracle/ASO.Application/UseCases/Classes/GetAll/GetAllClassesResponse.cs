using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Classes.GetAll;

public record GetAllClassesResponse : IResponse
{
    public IEnumerable<ClassDto> Classes { get; init; } = new List<ClassDto>();
}

public record ClassDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}