using System.Text;
using ASO.Application.Abstractions.Shared;
using ASO.Domain.AI.Abstractions.Repositories;
using ASO.Domain.AI.Dtos.ExternalServices;
using ASO.Domain.AI.Entities;
using ASO.Domain.AI.Enums;
using ASO.Domain.Game.Abstractions.ExternalServices;
using ASO.Domain.Game.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ASO.Application.UseCases.Oracle.GenerateCampaignStory;

public sealed class GenerateCampaignStoryFromCharactersHandler(
    IGeminiApiService geminiApiService,
    ICharacterRepository characterRepository,
    IGeneratedAIContentRepository generatedAIContentRepository
) : ICommandHandlerAsync<GenerateCampaignStoryFromCharactersCommand, GenerateCampaignStoryResponse>
{
    private readonly IGeminiApiService _geminiApiService = geminiApiService;
    private readonly ICharacterRepository _characterRepository = characterRepository;
    private readonly IGeneratedAIContentRepository _generatedAIContentRepository = generatedAIContentRepository;

    public async Task<GenerateCampaignStoryResponse> HandleAsync(GenerateCampaignStoryFromCharactersCommand command)
    {
        // Buscar personagens com suas relações
        var characters = await _characterRepository
            .GetAll()
            .Include(c => c.Image)
            .Include(c => c.Ancestry)
            .Include(c => c.Classes)
            .Where(c => command.CharacterIds.Contains(c.Id))
            .ToListAsync();

        if (characters.Count == 0)
        {
            throw new InvalidOperationException("Nenhum personagem encontrado com os IDs fornecidos.");
        }

        // Construir prompt
        var prompt = BuildCampaignStoryPrompt(
            command.CampaignName ?? "Nova Campanha",
            command.CampaignDescription ?? "Uma aventura épica aguarda os heróis.",
            characters.Select(c => new CharacterInfo(
                c.Name,
                c.Ancestry?.Name ?? "Desconhecida",
                c.Classes?.FirstOrDefault()?.Name ?? "Sem classe",
                c.Backstory ?? "Sem história definida."
            )).ToList()
        );

        // Chamar Gemini API
        var request = new GeminiServiceRequest(
            new List<ContentDto>
            {
                new(new List<Part> { new(prompt) })
            }
        );

        var response = await _geminiApiService.GenerateCampaignBackstoryAsync(request);
        var story = response.Candidates.FirstOrDefault()?.Content.Parts.FirstOrDefault()?.Text
                    ?? throw new InvalidOperationException("Falha ao gerar história da campanha.");

        // Salvar no banco
        var aiContent = GeneratedAIContent.Create(AIQueryType.CampaignBackstory, prompt, story);
        await _generatedAIContentRepository.Create(aiContent);

        return new GenerateCampaignStoryResponse
        {
            Story = story,
            Prompt = prompt
        };
    }

    private static string BuildCampaignStoryPrompt(
        string campaignName,
        string campaignDescription,
        List<CharacterInfo> characterInfo)
    {
        var sb = new StringBuilder();

        sb.AppendLine("# Contexto da Campanha");
        sb.AppendLine($"**Nome:** {campaignName}");
        sb.AppendLine($"**Descrição:** {campaignDescription}");
        sb.AppendLine();

        sb.AppendLine("# Personagens dos Jogadores");
        foreach (var character in characterInfo)
        {
            sb.AppendLine($"## {character.Name}");
            sb.AppendLine($"- **Ancestralidade:** {character.Ancestry}");
            sb.AppendLine($"- **Classe:** {character.Class}");
            sb.AppendLine($"- **História:** {character.Backstory ?? "Sem história definida."}");
            sb.AppendLine();
        }

        sb.AppendLine("# Mundo da Campanha");
        sb.AppendLine("(Mundo ainda não implementado - use informações genéricas de fantasia medieval)");
        sb.AppendLine();

        sb.AppendLine("# Tarefa");
        sb.AppendLine("Com base nas informações acima, crie o início da história desta campanha de RPG.");
        sb.AppendLine("A história deve:");
        sb.AppendLine("- Unir organicamente as histórias dos personagens");
        sb.AppendLine("- Criar um gancho narrativo que motive a aventura");
        sb.AppendLine("- Estabelecer o cenário inicial onde os personagens se encontram");
        sb.AppendLine("- Ter no máximo 500 palavras");
        sb.AppendLine("- Ser escrita em tom narrativo e envolvente");

        return sb.ToString();
    }
}
