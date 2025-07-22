using ASO.Domain.Game.ValueObjects;
using ASO.Domain.Shared.Messages;
using FluentValidation;
using Attribute = System.Attribute;

namespace ASO.Api.Inputs;

public sealed record CreateCharacterInput
{
    public string Name { get; set; } = string.Empty;
    public Guid AncestryId { get; set; }
    public List<Guid> SkillsIds { get; set; } = new();
    public Guid ClassId { get; set; } = Guid.Empty;
    public string Backstory { get; set; } = string.Empty;
    public ModifiersInput Modifiers { get; set; } = null!;
    public Guid? CampaignId { get; set; } = Guid.Empty;
    public Guid ImageId { get; set; } = Guid.Empty;
}

public record ModifiersInput
{
    public int ModStrength { get; set; }
    public int ModDexterity { get; set; }
    public int ModConstitution { get; set; }
    public int ModIntelligence { get; set; }
    public int ModWisdom { get; set; }
    public int ModCharisma { get; set; }
}

public class CreateCharacterInputValidator : AbstractValidator<CreateCharacterInput>
{
    public CreateCharacterInputValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ValidationMessages.CharacterNameRequired);

        RuleFor(x => x.AncestryId)
            .NotEmpty()
            .WithMessage(ValidationMessages.AncestryIdRequired);

        RuleFor(x => x.SkillsIds)
            .NotEmpty()
            .WithMessage("At least one expertise ID is required.");
    }
}