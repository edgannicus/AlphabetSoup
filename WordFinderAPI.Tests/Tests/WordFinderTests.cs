namespace WordFinderAPI.Tests.Tests;
public class WordFinderTests
{
    [Fact]
    public void FindWord_HorizontalWord_ReturnsWord()
    {
        // Arrange
        var matrix = new List<string>
        {
            "HELLOX",
            "WORLDQW",
            "EXAMPLE"
        };

        var wordStream = new List<string> { "HELLO" };
        var wordFinder = new WordFinder(matrix);

        // Act
        var result = wordFinder.Find(wordStream);

        // Assert
        Assert.Contains("HELLO", result);
    }
    
    [Fact]
    public void FindWord_VerticalWord_ReturnsWord()
    {
        // Arrange
        var matrix = new List<string>
        {
            "YWX",
            "EYO",
            "SLG"
        };

        var wordStream = new List<string> { "YES" };
        var wordFinder = new WordFinder(matrix);

        // Act
        var result = wordFinder.Find(wordStream);

        // Assert
        Assert.Contains("YES", result);
    }
    [Fact]
    public void FindWord_WordNotFound_ReturnsEmpty()
    {
        // Arrange
        var matrix = new List<string>
        {
            "TESTX",
            "WORLD",
            "XXXXX"
        };

        var wordStream = new List<string> { "HELLO" };
        var wordFinder = new WordFinder(matrix);

        // Act
        var result = wordFinder.Find(wordStream);

        // Assert
        Assert.DoesNotContain("HELLO", result);
    }
}