using System.Text.RegularExpressions;
using ASO.Domain.ValueObjects.Exceptions.Email;

namespace ASO.Domain.ValueObjects;

public sealed partial record Email : ValueObject
{
    #region Constants
    
    private const string Pattern = @"^\w+([\-+.'']\w+)*@\w+([\-\.]\w+)*\.\w+([\-\.]\w+)*$";
    public const int MaxLength = 60;
    public const int MinLength = 6;
    
    #endregion

    #region constructors

    private Email(string address)
    {
        Address = address;
    }

    #endregion
    
    #region Factory Methods
    
    public static Email Create(string address)
    {
        if (string.IsNullOrEmpty(address)
            || string.IsNullOrWhiteSpace(address))
            throw new InvelidEmailLenghtException("Email cannot be null or empty");
        
        address = address.Trim();
        address = address.ToLower();
        
        if (!EmailRegex().IsMatch(address))
            throw new InvelidEmailException("invalid email address");
        
        return new Email(address);
    }
    
    #endregion
    
    #region Properties

    public string Address { get; }
    
    #endregion

    #region Source Generator
    
    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();

    #endregion
}