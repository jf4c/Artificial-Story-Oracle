using ASO.Application.UseCases.Characters.Create;
using ASO.Domain.Game.ValueObjects;

namespace ASO.Api.Inputs.Mappers;

public static class CreateCharacterInputMapper
{
    public static CreateCharacterCommand ToCommand(this CreateCharacterInput input)
    {
        return new CreateCharacterCommand
        {
            Name = input.Name,
            AncestryId = input.AncestryId,
            SkillsIds = input.SkillsIds,
            ClasseId = input.ClassId,
            Modifiers = AttributeModifiers.Create(input.Attributes.ModStrength,
                input.Attributes.ModDexterity,
                input.Attributes.ModConstitution, 
                input.Attributes.ModIntelligence,
                input.Attributes.ModWisdom, 
                input.Attributes.ModCharisma),
        };
    }
}