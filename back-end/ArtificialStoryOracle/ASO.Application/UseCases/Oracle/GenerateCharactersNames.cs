using ASO.Application.Abstractions.UseCase.Oracle;

namespace ASO.Application.UseCases.Oracle;

public class GenerateCharactersNames : IGenerateCharactersNames
{
    public Task<AIDataGeneratorResponse> HandleAsync(AIDataGeneratorCommand command)
    {
        throw new NotImplementedException();
    }
}