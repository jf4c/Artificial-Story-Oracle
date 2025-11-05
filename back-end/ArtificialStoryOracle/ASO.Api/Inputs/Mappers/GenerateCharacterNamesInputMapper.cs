using ASO.Application.UseCases.Oracle.GenerateNames;

namespace ASO.Api.Inputs.Mappers;

public static class GenerateCharacterNamesInputMapper
{
    public static GenerateCharacterNamesCommand ToCommand(this GenerateCharacterNamesInput input)
    {
        return new GenerateCharacterNamesCommand
        {
            AncestryId = input.AncestryId,
            ClassId = input.ClassId
        };
    }
}

