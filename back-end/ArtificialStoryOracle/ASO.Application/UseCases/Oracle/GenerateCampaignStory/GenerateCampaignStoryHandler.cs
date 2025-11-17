using ASO.Application.Abstractions.Shared;
using ASO.Domain.AI.Abstractions.Repositories;
using ASO.Domain.AI.Dtos.ExternalServices;
using ASO.Domain.AI.Entities;
using ASO.Domain.AI.Enums;
using ASO.Domain.Game.Abstractions.ExternalServices;
using ASO.Domain.Game.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ASO.Application.UseCases.Oracle.GenerateCampaignStory;

public sealed class GenerateCampaignStoryHandler(
    IGeminiApiService geminiApiService,
    ICampaignRepository campaignRepository,
    IGeneratedAIContentRepository aiContentRepository) 
    : ICommandHandlerAsync<GenerateCampaignStoryCommand, GenerateCampaignStoryResponse>
{
    private readonly IGeminiApiService _geminiApiService = geminiApiService;
    private readonly ICampaignRepository _campaignRepository = campaignRepository;
    private readonly IGeneratedAIContentRepository _aiContentRepository = aiContentRepository;

    public async Task<GenerateCampaignStoryResponse> HandleAsync(GenerateCampaignStoryCommand command)
    {
        // Buscar a campanha com participantes e personagens
        var campaign = await _campaignRepository.GetByIdWithParticipantsAsync(command.CampaignId)
            ?? throw new InvalidOperationException("Campanha não encontrada.");

        // Coletar backstories dos personagens
        var characterBackstories = campaign.Participants
            .Where(p => p.IsActive && p.Character != null)
            .Select(p => new
            {
                Name = p.Character!.Name,
                Ancestry = p.Character.Ancestry.Name,
                Class = p.Character.Classes?.FirstOrDefault()?.Name ?? "Aventureiro",
                Backstory = p.Character.Backstory ?? "Um aventureiro misterioso."
            })
            .ToList();

        // Construir o prompt
        var prompt = BuildCampaignStoryPrompt(
            campaign.Name, 
            campaign.Description, 
            characterBackstories.Select(c => new CharacterInfo(
                c.Name,
                c.Ancestry,
                c.Class,
                c.Backstory
            )).ToList()
        );

        // Chamar a API do Gemini
        var request = new GeminiServiceRequest(
            new List<ContentDto>
            {
                new(new List<Part> { new(prompt) })
            }
        );

        var response = await _geminiApiService.GenerateCampaignBackstoryAsync(request);
        var storyText = response.Candidates.FirstOrDefault()?.Content.Parts.FirstOrDefault()?.Text
            ?? throw new InvalidOperationException("Falha ao gerar história da campanha.");

        // Salvar no histórico de conteúdo gerado pela IA
        var aiContent = GeneratedAIContent.Create(
            AIQueryType.CampaignBackstory,
            prompt,
            storyText);

        await _aiContentRepository.Create(aiContent);

        return new GenerateCampaignStoryResponse
        {
            Story = storyText,
            Prompt = prompt
        };
    }

    private string BuildCampaignStoryPrompt(string campaignName, string? campaignDescription, List<CharacterInfo> characterBackstories)
    {
        var charactersInfo = string.Join("\n\n", characterBackstories.Select((c, index) => 
            $"**Personagem {index + 1}: {c.Name}**\n" +
            $"- Raça: {c.Ancestry}\n" +
            $"- Classe: {c.Class}\n" +
            $"- História: {c.Backstory}"));

        var worldDescription = "Um mundo de fantasia medieval repleto de magia, criaturas místicas e reinos antigos. " +
                              "Dungeons escondidas guardam tesouros e perigos, enquanto vilas e cidades prosperam sob a proteção de heróis corajosos.";

        return $@"Você é um mestre de RPG experiente. Crie o início épico de uma campanha de RPG chamada ""{campaignName}"".

**Descrição da Campanha:**
{campaignDescription ?? "Uma aventura épica está prestes a começar."}

**Contexto do Mundo:**
{worldDescription}

**Personagens Envolvidos:**
{charactersInfo}

**Tarefa:**
Escreva o início da história da campanha em até 500 palavras. A história deve:
1. Unir as histórias individuais dos personagens de forma coesa
2. Estabelecer um gancho inicial que motive os personagens a se unirem
3. Criar tensão e mistério
4. Definir o cenário inicial onde a aventura começa
5. Terminar com um momento dramático que leve à ação

Escreva de forma narrativa, envolvente e cinematográfica, como se fosse a introdução de um filme ou o prólogo de um livro épico de fantasia.";
    }
}

public record CharacterInfo(string Name, string Ancestry, string Class, string Backstory);
