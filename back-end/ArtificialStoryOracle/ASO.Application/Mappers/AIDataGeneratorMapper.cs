using ASO.Application.UseCases.Oracle;
using ASO.Domain.AI.Dtos.ExternalServices;
using ASO.Domain.AI.Entities;

namespace ASO.Application.Mappers;

public static class AIDataGeneratorMapper
{
    public static GeminiServiceRequest ToOpenAIServiceRequest(
        this AIDataGeneratorCommand command)
    {
        var content = ContentDto.Generate(
            command.Name, 
            command.Ancestry, 
            command.Class,
            command.Attributes,
            command.Skills,
            command.Supplements);
        
        return new GeminiServiceRequest([content]);
    }
    
    public static AIDataGeneratorResponse ToAIDataGeneratorResponse(
        this GeneratedAIContent entity)
    {
        return new AIDataGeneratorResponse
        {
            Text = entity.Content
        };
    }
}