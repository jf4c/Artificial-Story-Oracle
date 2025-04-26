using ASO.Domain.ValueObjects;
using ASO.Domain.ValueObjects.Exceptions;
using ASO.Domain.ValueObjects.Exceptions.Name;

namespace ASO.Domain.Tests.ValueObjects;

public class NameTest
{
    // Arrange
    private const string _firstName = "Julio";
    private const string _lastName = "Costa";
    
    // Act
    // private readonly Name _name = new Name(_firstName, _lastName);
    private readonly Name _name = Name.Create(_firstName, _lastName);
    
    
    [Fact]
    public void ShouldOverrideToStringMethod()
    {
        // Assert
        Assert.Equal("Julio Costa", _name.ToString());
    }
    
    [Fact]
    public void ShouldImplicitConvertToString()
    {
        // Act
        string nameString = _name;

        // Assert
        Assert.Equal("Julio Costa", nameString);
    }
    
    [Fact]
    public void ShouldCreateNameWithValidParameters()
    {

        // Act
        var name = Name.Create(_firstName, _lastName);

        // Assert
        Assert.Equal(_firstName, name.FirstName);
        Assert.Equal(_lastName, name.LastName);
    }
    
    [Fact]
    public void ShouldFailIfFirstNameLenghtIsNotValid()
    {
        Assert.Throws<InvalidFirstNameLenghtException>(() => Name.Create("a", _lastName));
    }
    
    [Fact]
    public void ShouldFailIfLastNameLenghtIsNotValid()
    {
        Assert.Throws<InvalidLastNameLenghtException>(() => Name.Create(_firstName, "a"));
    }
    
    
}