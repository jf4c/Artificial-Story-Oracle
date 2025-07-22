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
            Modifiers = AttributeModifiers.Create(input.Modifiers.ModStrength,
                input.Modifiers.ModDexterity,
                input.Modifiers.ModConstitution, 
                input.Modifiers.ModIntelligence,
                input.Modifiers.ModWisdom, 
                input.Modifiers.ModCharisma),
            Backstory = input.Backstory,
            ImageId = input.ImageId
        };
    }
}