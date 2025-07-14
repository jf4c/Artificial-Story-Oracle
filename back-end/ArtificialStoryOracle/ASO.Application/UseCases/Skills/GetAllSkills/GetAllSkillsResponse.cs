using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Skills.GetAllSkills;

public sealed record GetAllSkillsResponse : IResponse
{
    public IEnumerable<SkillDto> Skills { get; init; } = new List<SkillDto>();
}

public sealed record SkillDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
}