using ASO.Domain.ValueObjects.Exceptions;
using ASO.Domain.ValueObjects.Exceptions.Name;

namespace ASO.Domain.ValueObjects;

public sealed record Name : ValueObject
{
    #region Constants
    
    public const int MaxLength = 60;
    public const int MinLength = 3;
    
    #endregion
    
    #region Constructors
    
    private Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
    
    #endregion
    
    #region Factory Methods
    
    public static Name Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) 
            || string.IsNullOrWhiteSpace(lastName)
            || string.IsNullOrEmpty(firstName) 
            || string.IsNullOrEmpty(lastName))
            throw new InvalidNameException("First name and Last name cannot be null or empty.");
        
        if (firstName.Length is < MinLength or > MaxLength)
            throw new InvalidFirstNameLenghtException($"First name must be between {MinLength} and {MaxLength} characters.");
        
        if (lastName.Length is < MinLength or > MaxLength)
            throw new InvalidLastNameLenghtException($"Last name must be between {MinLength} and {MaxLength} characters.");

        return new Name(firstName, lastName);
    }
    
    #endregion
    
    #region Properties

    public string FirstName { get; }
    public string LastName { get; }

    #endregion

    #region Operators

    public static implicit operator string(Name name) => name.ToString();

    #endregion

    #region Overrides
    
    public override string ToString() => $"{FirstName} {LastName}";
    
    #endregion
    
}