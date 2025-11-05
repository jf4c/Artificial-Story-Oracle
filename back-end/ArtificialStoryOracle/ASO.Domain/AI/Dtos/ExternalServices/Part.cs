﻿namespace ASO.Domain.AI.Dtos.ExternalServices;

public record Part(string Text)
{
    public static Part GenerateUserPrompt(
        string name, 
        string ancestry, 
        string @class,
        string attributes,
        string skills,
        string supplements) =>
        new($"""
              Gere uma história de fundo para um personagem de RPG de fantasia com os atributos:
              
              Nome: {name}
              Ancestralidade: {ancestry}
              Classe: {@class}
              Atributos: {attributes}
              Perícias: {skills}
              Informações complementares: {supplements}
              
              Inclua na história: infância, motivações, objetivos e 
              como os atributos e perícias influenciam a vida do personagem 
              (não precisa colocar os valores).
              Resuma em até 200 palavras.
              """);
    
    public static Part GenerateCharacterNamesPrompt(string? ancestry, string? @class) =>
        new($"""
              Gere exatamente 10 nomes criativos e únicos para personagens de RPG de fantasia.
              
              {(string.IsNullOrWhiteSpace(ancestry) ? "" : $"Ancestralidade: {ancestry}")}
              {(string.IsNullOrWhiteSpace(@class) ? "" : $"Classe: {@class}")}
              
              Requisitos:
              1. Gere 5 nomes MASCULINOS e 5 nomes FEMININOS
              2. Os nomes devem ser apropriados para um cenário de fantasia medieval
              {(string.IsNullOrWhiteSpace(ancestry) ? "" : "3. Os nomes devem refletir a ancestralidade especificada")}
              {(string.IsNullOrWhiteSpace(@class) ? "" : "4. Os nomes devem ser adequados para a classe especificada")}
              3. Devem ser nomes completos (nome + sobrenome quando apropriado)
              4. Forneça apenas os nomes, um por linha
              5. Não adicione numeração, explicações ou comentários
              6. Primeiro liste os 5 nomes masculinos, depois os 5 femininos
              
              Formato de resposta (um nome por linha, sem numeração):
              [Nome masculino 1]
              [Nome masculino 2]
              [Nome masculino 3]
              [Nome masculino 4]
              [Nome masculino 5]
              [Nome feminino 1]
              [Nome feminino 2]
              [Nome feminino 3]
              [Nome feminino 4]
              [Nome feminino 5]
              """);
}