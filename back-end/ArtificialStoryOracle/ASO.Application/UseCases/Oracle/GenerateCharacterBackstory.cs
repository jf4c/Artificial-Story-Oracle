using ASO.Application.Abstractions.UseCase.Oracle;
using ASO.Application.Mappers;
using ASO.Domain.AI.Abstractions.Repositories;
using ASO.Domain.AI.Dtos.ExternalServices;
using ASO.Domain.AI.Entities;
using ASO.Domain.AI.Enums;
using ASO.Domain.Game.Abstractions.ExternalServices;

namespace ASO.Application.UseCases.Oracle;

public class GenerateCharacterBackstory(
    IGeminiApiService geminiApiServices, 
    IGeneratedAIContentRepository repository ) : IGenerateCharacterBackstory
{
    private readonly IGeminiApiService _geminiExternalService = geminiApiServices;
    private readonly IGeneratedAIContentRepository _repository = repository;
    
    public async Task<AIDataGeneratorResponse> HandleAsync(AIDataGeneratorCommand command)
    {
        var prompt = Part.GenerateUserPrompt(
            command.Name, 
            command.Ancestry, 
            command.Class,
            command.Supplements);
        
        var request = command.ToOpenAIServiceRequest();
        
        var responseIA = await _geminiExternalService.GenerateCampaignBackstoryAsync(request);
        
        var text = responseIA.Candidates.FirstOrDefault()?.Content.Parts.FirstOrDefault()?.Text;
        
        var aiContent = GeneratedAIContent.Create(AIQueryType.CharacterBackstory, prompt.Text, text!);
        
        await _repository.Create(aiContent);
        
        return aiContent.ToAIDataGeneratorResponse();
    }
}