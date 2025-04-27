using ASO.Domain.ValueObjects.Exceptions.Nickname;

namespace ASO.Domain.ValueObjects;

public sealed record Nickname : ValueObject
{
    #region Constants
    
    public const int MaxLength = 20;
    public const int MinLength = 20;
    
    #endregion
    
    #region Constructors
    
    private Nickname(string nick)
    {
        Nick = nick;
    }
    
    #endregion
    
    #region Factory Methods
    
    public static Nickname Create(string nick)
    {
        if (string.IsNullOrWhiteSpace(nick) || string.IsNullOrEmpty(nick))
            throw new InvalidNicknameException("Nickname cannot be null or empty.");
        
        if (nick.Length is < MinLength or > MaxLength)
            throw new InvalidNicknameLenghtException($"Nickname must be between {MinLength} and {MaxLength} characters.");
        
        return new Nickname(nick);
    }
    
    #endregion
    
    #region Properties
    
    public string Nick { get; }
    
    #endregion
}
