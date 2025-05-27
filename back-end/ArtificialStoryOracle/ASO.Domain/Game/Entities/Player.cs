using ASO.Domain.Game.Enums;
using ASO.Domain.Shared.Aggregates.Abstractions;
using ASO.Domain.Shared.Entities;
using ASO.Domain.Shared.ValueObjects;
using Email = ASO.Domain.Shared.ValueObjects.Email;

namespace ASO.Domain.Game.Entities;

public class Player : Entity, IAggragateRoot
{
    #region Constructors
    
    private Player()
    {
        Name = null!;
        Email = null!;
        NickName = null!;
        TypePlayer = TypePlayer.Player;
    }

    private Player(string firstName, string lastName, string address, string nickName)
    {
        Name = Name.Create(firstName, lastName);
        Email = Email.Create(address);
        NickName = Nickname.Create(nickName);
        TypePlayer = TypePlayer.Player;
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
    public TypePlayer TypePlayer { get; }
    
    #endregion
}