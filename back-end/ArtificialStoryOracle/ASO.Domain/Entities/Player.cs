using ASO.Domain.ValueObjects;

namespace ASO.Domain.Entities;

public class Player : Entity
{
    #region Constructors

    private Player(string firstName, string lastName, string address, string nickName) : base(Guid.NewGuid(), Tracker.Create())
    {
        Name = Name.Create(firstName, lastName);
        Email = Email.Create(address);
        NickName = Nickname.Create(nickName);
    }
    
    #endregion
    
    #region Factory Methods
    
    public static Player Create(string firstName, string lastName, string address, string nick) 
        => new(firstName, lastName, address, nick);

    #endregion

    #region Proporties
    
    public Name Name { get; }
    public Email Email { get; }
    public Nickname NickName { get; }
    public string Password { get; } = string.Empty;
    
    #endregion
}