﻿using ASO.Application.UseCases.Oracle;
using ASO.Application.UseCases.Oracle.GenerateNames;
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
    
    public static GeminiServiceRequest ToGeminiServiceRequest(
        this GenerateCharacterNamesCommand command,
        string? ancestryName,
        string? className)
    {
        var prompt = Part.GenerateCharacterNamesPrompt(ancestryName, className);
        var content = new ContentDto([prompt]);
        
        return new GeminiServiceRequest([content]);
    }
    
    public static GenerateCharacterNamesResponse ToGenerateCharacterNamesResponse(
        this GeneratedAIContent entity, 
        List<string> maleNames, 
        List<string> femaleNames,
        string? ancestry,
        string? @class)
    {
        return new GenerateCharacterNamesResponse
        {
            MaleNames = maleNames,
            FemaleNames = femaleNames,
            Ancestry = ancestry,
            Class = @class,
            GeneratedAt = entity.Tracker.CreatedAtUtc
        };
    }
}

