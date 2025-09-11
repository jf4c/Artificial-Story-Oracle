using ASO.Domain.AI.Enums;
using ASO.Domain.Shared.Aggregates.Abstractions;
using ASO.Domain.Shared.Entities;

namespace ASO.Domain.AI.Entities;

public class GeneratedAIContent : Entity, IAggragateRoot
{
    #region Constructors
    private GeneratedAIContent() { }

    private GeneratedAIContent(AIQueryType type, string prompt, string content)
    {
        Type = type;
        Prompt = prompt;
        Content = content;
    }
    #endregion

    #region Factory Methods
    public static GeneratedAIContent Create(AIQueryType type, string prompt, string content)
    {
        return new GeneratedAIContent(type, prompt, content);
    }
    #endregion

    #region Properties
    public AIQueryType Type { get; } = AIQueryType.CharacterBackstory;
    public string Prompt { get; } = string.Empty;
    public string Content { get; } = string.Empty;
    #endregion
}