using ASO.Application.Abstractions.UseCase.Oracle;

namespace ASO.Application.UseCases.Oracle;

public class GenerateCampaignBackstory : IGenerateCampaignBackstory
{
    public Task<AIDataGeneratorResponse> HandleAsync(AIDataGeneratorCommand command)
    {
        throw new NotImplementedException();
    }
}