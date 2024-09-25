public class WordFinderTests
{
    [Fact]
    public void FindWordsInMatrix_Example_ReturnsCorrectWords()
    {
        // Matriz basada en el ejemplo proporcionado
        var matrix = new List<string> 
        { 
            "coldxx", 
            "rwindo", 
            "schill", 
            "pqnnsd", 
            "uvdxyi", 
            "abckll"
        };

        // Palabras del ejemplo
        var words = new List<string> { "chill", "cold", "wind" };  
        var wordFinder = new WordFinder(6);  // Tamaño de la matriz 6x6

        // Act
        var result = wordFinder.Find(words);

        // Assert
        Assert.Contains("chill", result);  // Verificamos que "chill" esté en el resultado
        Assert.Contains("cold", result);   // Verificamos que "cold" esté en el resultado
        Assert.Contains("wind", result);   // Verificamos que "wind" esté en el resultado
    }
    /*[Fact]
    public void FindWordsInMatrix_Example_ReturnsCorrectWords()
    {
        // Matriz basada en el ejemplo
        var matrix = new List<string> 
        { 
            "abcclo", 
            "rwindo", 
            "schill", 
            "pqnnsd", 
            "uvdxy" 
        };

        // Palabras del ejemplo
        var words = new List<string> { "chill", "cold", "wind" };  
        var wordFinder = new WordFinder(6);  // Asegúrate de que el tamaño de la matriz es correcto

        // Act
        var result = wordFinder.Find(words);

        // Assert
        Assert.Contains("chill", result);
        Assert.Contains("cold", result);
        Assert.Contains("wind", result);
    }*/
    [Fact]
    public void FindWordsInEmptyMatrix_ReturnsEmpty()
    {
        // Usamos una lista vacía en lugar de un tamaño de 0
        var matrix = new List<string>();  // Matriz vacía
        var words = new List<string> { "abc" };
        var wordFinder = new WordFinder(1);  // Tamaño mínimo de 1

        // Act
        var result = wordFinder.Find(words);

        // Assert
        Assert.Empty(result);  // Esperamos que no se encuentre ninguna palabra
    }
    [Fact]
    public void FindWordsNotInMatrix_ReturnsEmpty()
    {
        // Arrange
        var matrix = new List<string> 
        { 
            "abcd", 
            "efgh", 
            "ijkl", 
            "mnop" 
        };
        var words = new List<string> { "xyz", "uvw" };
        var wordFinder = new WordFinder(4);

        // Act
        var result = wordFinder.Find(words);

        // Assert
        Assert.Empty(result);  // Esperamos un conjunto vacío
    }
    [Fact]
    public void FindWordsInMaxMatrixSize_ReturnsCorrectWords()
    {
        // Generamos una matriz predefinida de tamaño máximo (64x64)
        var matrix = new List<string>();
        for (int i = 0; i < 64; i++)
        {
            matrix.Add(new string('a', 64));  // Llenamos la matriz con 'a'
        }

        var words = new List<string> { "aaaa", "aa" };  // Sabemos que estas palabras existen
        var wordFinder = new WordFinder(64);  // Matriz de 64x64

        // Act
        var result = wordFinder.Find(words);

        // Assert
        Assert.Contains("aaaa", result);  // Verificamos que "aaaa" esté en el resultado
        Assert.Contains("aa", result);    // Verificamos que "aa" esté en el resultado
    }
    /*[Fact]
    public void FindWordsInMaxMatrixSize_ReturnsCorrectWords()
    {
        // Arrange
        var matrix = new List<string>();
        for (int i = 0; i < 64; i++)
        {
            matrix.Add(new string('a', 64));  // Matriz llena de letras 'a'
        }

        var words = new List<string> { "aaaa", "aa" };
        var wordFinder = new WordFinder(64);

        // Act
        var result = wordFinder.Find(words);

        // Assert
        Assert.Contains("aaaa", result);
        Assert.Contains("aa", result);
    }*/
    [Fact]
    public void WordFinder_ThrowsException_WhenMatrixSizeIsInvalid()
    {
        // Validamos que el tamaño 0 arroje una excepción
        Assert.Throws<ArgumentException>(() => new WordFinder(0));
    
        // Validamos que un tamaño mayor a 64 arroje una excepción
        Assert.Throws<ArgumentException>(() => new WordFinder(65));
    }
}