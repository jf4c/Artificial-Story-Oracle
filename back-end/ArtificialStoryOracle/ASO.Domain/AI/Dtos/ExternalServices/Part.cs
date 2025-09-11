namespace ASO.Domain.AI.Dtos.ExternalServices;

public record Part(string Text)
{
    public static Part GenerateUserPrompt(
        string name, 
        string ancestry, 
        string @class, 
        string supplements) =>
        new($"""
             Gere uma história de fundo para um personagem de RPG de fantasia com os atributos:
             Nome: {name}
             Ancestralidade: {ancestry}
             Classe: {@class}
             Informações complementares: {supplements}
             Inclua: infância, motivações, objetivos.
             Resuma em até 200 palavras.
             """);
}