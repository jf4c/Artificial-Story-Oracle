using ASO.Application.UseCases.Oracle;

namespace ASO.Api.Inputs.Mappers;

public static class AIDataGeneratorInputMapper
{
    public static AIDataGeneratorCommand ToCommand(this AIDataGeneratorInput input)
    {
        return new AIDataGeneratorCommand
        {
            Name = input.Name,
            Ancestry = input.Ancestry,
            Class = input.Class,
            Attributes = input.Attributes,
            Skills = input.Skills,
            Supplements = input.Supplements,
        };
    }
    
}