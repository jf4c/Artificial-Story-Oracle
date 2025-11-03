namespace ASO.Domain.AI.Dtos.ExternalServices;

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
}