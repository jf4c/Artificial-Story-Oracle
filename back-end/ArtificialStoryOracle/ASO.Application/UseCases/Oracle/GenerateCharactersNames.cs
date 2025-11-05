﻿using ASO.Application.Abstractions.UseCase.Oracle;
using ASO.Application.Mappers;
using ASO.Application.UseCases.Oracle.GenerateNames;
using ASO.Domain.AI.Abstractions.Repositories;
using ASO.Domain.AI.Entities;
using ASO.Domain.AI.Enums;
using ASO.Domain.Game.Abstractions.ExternalServices;
using ASO.Domain.Game.Abstractions.QueriesServices;

namespace ASO.Application.UseCases.Oracle;

public sealed class GenerateCharactersNames(
    IGeminiApiService geminiApiService,
    IGeneratedAIContentRepository repository,
    IAncestryQueryService ancestryQueryService,
    IClassQueryService classQueryService) : IGenerateCharactersNames
{
    private readonly IGeminiApiService _geminiApiService = geminiApiService;
    private readonly IGeneratedAIContentRepository _repository = repository;
    private readonly IAncestryQueryService _ancestryQueryService = ancestryQueryService;
    private readonly IClassQueryService _classQueryService = classQueryService;
    
    public async Task<GenerateCharacterNamesResponse> HandleAsync(GenerateCharacterNamesCommand command)
    {
        // Buscar Ancestry e Class no banco se IDs foram fornecidos
        string? ancestryName = null;
        string? className = null;
        
        if (command.AncestryId.HasValue)
        {
            var ancestry = await _ancestryQueryService.GetById(command.AncestryId.Value);
            ancestryName = ancestry?.Name;
        }
        
        if (command.ClassId.HasValue)
        {
            var classEntity = await _classQueryService.GetById(command.ClassId.Value);
            className = classEntity?.Name;
        }
        
        // Montar request para Gemini API
        var request = command.ToGeminiServiceRequest(ancestryName, className);
        
        // Chamar Gemini API
        var responseIa = await _geminiApiService.GenerateCharacterNamesAsync(request);
        
        // Extrair texto da resposta
        var text = responseIa.Candidates.FirstOrDefault()?.Content.Parts.FirstOrDefault()?.Text;
        
        if (string.IsNullOrWhiteSpace(text))
            throw new Exception("Gemini API retornou resposta vazia");
        
        // Parsear os nomes (split por linha e remover vazios)
        var allNames = text
            .Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Trim())
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.TrimStart('1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '.', '-', ' '))
            .ToList();
        
        // Dividir em masculinos (primeiros 5) e femininos (últimos 5)
        var maleNames = allNames.Take(5).ToList();
        var femaleNames = allNames.Skip(5).Take(5).ToList();
        
        // Garantir que temos exatamente 5 de cada
        while (maleNames.Count < 5)
            maleNames.Add($"Nome Masculino {maleNames.Count + 1}");
        
        while (femaleNames.Count < 5)
            femaleNames.Add($"Nome Feminino {femaleNames.Count + 1}");
        
        // Criar prompt que foi usado (para salvar no banco)
        var promptText = $"Gerar nomes de personagens - Ancestralidade: {ancestryName ?? "Não especificada"}, Classe: {className ?? "Não especificada"}";
        
        // Salvar conteúdo gerado no banco
        var aiContent = GeneratedAIContent.Create(AIQueryType.CharacterNames, promptText, text);
        await _repository.Create(aiContent);
        
        // Retornar response
        return aiContent.ToGenerateCharacterNamesResponse(maleNames, femaleNames, ancestryName, className);
    }
}