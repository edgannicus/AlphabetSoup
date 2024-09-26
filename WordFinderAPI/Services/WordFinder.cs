public class WordFinder : IWordFinder
{
    private readonly List<string> _matrix;

    // Constructor que solo almacena la matriz
    public WordFinder(IEnumerable<string> matrix)
    {
        _matrix = matrix.ToList();  // Ahora guardamos la matriz que el controlador pasa correctamente
    }

    // Find function to search for words in the matrix
    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        var foundWords = new HashSet<string>();  // To store unique found words

        foreach (var word in wordstream)
        {
            if (SearchWord(word))  // Search for each word
            {
                foundWords.Add(word);
            }

            // Stop searching after finding 10 unique words
            if (foundWords.Count == 10)
            {
                break;
            }
        }

        return foundWords;  // Return the found words
    }

    // Function to search for a word in the matrix (horizontally and vertically)
    private bool SearchWord(string word)
    {
        // Search horizontally
        for (int row = 0; row < _matrix.Count; row++)
        {
            if (_matrix[row].Contains(word))
            {
                return true;  // Found word horizontally
            }
        }

        // Search vertically
        for (int col = 0; col < _matrix[0].Length; col++)
        {
            string verticalWord = "";

            for (int row = 0; row < _matrix.Count; row++)
            {
                verticalWord += _matrix[row][col];  // Build vertical word character by character
            }

            if (verticalWord.Contains(word))
            {
                return true;  // Found word vertically
            }
        }

        return false;  // Word not found
    }
}