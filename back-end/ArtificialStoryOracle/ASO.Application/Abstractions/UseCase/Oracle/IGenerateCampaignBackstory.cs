using ASO.Application.Abstractions.Shared;
using ASO.Application.UseCases.Oracle;

namespace ASO.Application.Abstractions.UseCase.Oracle;

public interface IGenerateCampaignBackstory : ICommandHandlerAsync<AIDataGeneratorCommand, AIDataGeneratorResponse>;