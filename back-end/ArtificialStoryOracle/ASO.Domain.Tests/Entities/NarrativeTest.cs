using ASO.Domain.Entities;

namespace ASO.Domain.Tests.Entities;

public class NarrativeTest
{
    [Fact]
    public void Test1()
    {
        var narrative = new Narrative();
        
        Assert.Equal("Test Title", narrative.Titule);
    }
}