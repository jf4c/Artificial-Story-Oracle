using ASO.Application.Abstractions.Shared;

namespace ASO.Application.UseCases.Characters.Create;

public sealed record CreateCharacterCommand : ICommand
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Guid AncestryId { get; set; }
    public List<Guid> ExpertisesIds { get; set; } = new();
    public List<Guid> ClassesIds { get; set; } = new();
}