using ASO.Domain.ValueObjects;

namespace ASO.Domain.Entities;

public class Player : Entity
{
    #region Constructors

    public Player(string firstName, string lastName, string address) : base(Guid.NewGuid())
    {
        Name = Name.Create(firstName, lastName);
        Email = Email.Create(address);
    }
    
    #endregion
    
    public Name Name { get; }
    public Email Email { get; }
    
    public string NickName { get; } = string.Empty;
    public string Password { get; } = string.Empty;
}