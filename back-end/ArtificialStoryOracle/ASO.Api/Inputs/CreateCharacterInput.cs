namespace ASO.Api.Inputs;

public sealed record CreateCharacterInput
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Guid AncestryId { get; set; }
    public List<Guid> ExpertisesIds { get; set; } = new();
    public List<Guid> ClassesIds { get; set; } = new();
}