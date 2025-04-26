using ASO.Domain.ValueObjects;
using ASO.Domain.ValueObjects.Exceptions.Email;

namespace ASO.Domain.Tests.ValueObjects;

public class EmailTest
{
    [Theory]
    [InlineData("teste@hotmail.com")]
    [InlineData("teste@gmail.org")]
    [InlineData("teste12@gmail.io")]
    public void ShouldCreateAnEmail(string address)
    {
        // Act
        var email = Email.Create(address);
        
        // Assert
        Assert.Equal(email.Address, address);
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    public void ShouldFailToCreateAnEmail(string address)
    {   
        Assert.Throws<InvelidEmailLenghtException>(() => Email.Create(address));
    }
    
    [Theory]
    [InlineData("dsfsdfsdfsdf")]
    [InlineData("##@gmail.com")]
    public void ShouldFailToCreateAnEmailIsAddressIsNotValid(string address)
    {   
        Assert.Throws<InvelidEmailException>(() => Email.Create(address));
    }
}