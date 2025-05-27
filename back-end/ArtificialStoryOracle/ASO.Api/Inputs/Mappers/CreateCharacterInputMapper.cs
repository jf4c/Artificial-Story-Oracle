using ASO.Application.UseCases.Characters.Create;

namespace ASO.Api.Inputs.Mappers;

public static class CreateCharacterInputMapper
{
    public static CreateCharacterCommand ToCommand(this CreateCharacterInput input)
    {
        return new CreateCharacterCommand
        {
            FirstName = input.FirstName,
            LastName = input.LastName,
            AncestryId = input.AncestryId,
            ExpertisesIds = input.ExpertisesIds,
            ClassesIds = input.ClassesIds
        };
    }
}