namespace WordFinderAPI.Services
{
    public class WordFinder
    {
        private readonly char[,] _board;
        private readonly char[,] _resultMatrix;
        private readonly List<string> _matrix;
        private readonly List<string> _columns;
        private readonly Random _random = new Random();
        
        // Regex to validate only uppercase letters
        private static readonly Regex UpperCaseLetterRegex = new Regex("^[A-Z]+$", RegexOptions.Compiled);

        // Constructor that receives a matrix or uses a default if input is null or empty
        public WordFinder(IEnumerable<string>? matrixInput)
        {
            if (matrixInput == null || !matrixInput.Any() || !IsValidMatrix(matrixInput))
            {
                // Use the default matrix if input is null, empty or invalid
                _matrix = WordFinderDefaults.DefaultMatrix.ToList();
            }
            else
            {
                // Validate that the input contains only uppercase letters
                _matrix = matrixInput.Where(row => ValidateUpperCase(row)).ToList();
            }

            var matrixSize = _matrix.Max(row => row.Length);

            // Initialize the board and resultMatrix with '0'
            _board = new char[_matrix.Count, matrixSize];
            _resultMatrix = new char[_matrix.Count, matrixSize];
            FillBoardWithMatrix();
            
            // Prepare columns for vertical search
            _columns = new List<string>();
            if (_matrix.Count > 0)
            {
                for (int col = 0; col < _matrix[0].Length; col++)
                {
                    string column = "";
                    for (int row = 0; row < _matrix.Count; row++)
                    {
                        column += _board[row, col];
                    }
                    _columns.Add(column);
                }
            }
        }

        // Fill the board with the provided matrix
        private void FillBoardWithMatrix()
        {
            for (int row = 0; row < _matrix.Count; row++)
            {
                var matrixRow = _matrix[row];
                for (int col = 0; col < matrixRow.Length; col++)
                {
                    _board[row, col] = matrixRow[col];
                    _resultMatrix[row, col] = '0';
                }
            }

            // Fill empty spaces in the board with random letters
            FillBoardWithRandomLetters();
        }

        // Generate dynamic A-Z letters and fill empty spaces
        private void FillBoardWithRandomLetters()
        {
            for (int row = 0; row < _board.GetLength(0); row++)
            {
                for (int col = 0; col < _board.GetLength(1); col++)
                {
                    if (_board[row, col] == '\0') // Only fill empty spaces
                    {
                        _board[row, col] = GenerateRandomUpperCaseLetter();
                    }
                }
            }
        }

        // Method to generate a random uppercase letter (A-Z)
        private char GenerateRandomUpperCaseLetter()
        {
            return (char)_random.Next('A', 'Z' + 1);
        }

        // Method to validate that the input string contains only uppercase letters
        private bool ValidateUpperCase(string input)
        {
            if (!UpperCaseLetterRegex.IsMatch(input))
            {
                throw new ArgumentException($"Invalid string '{input}'. Only uppercase letters are allowed.");
            }
            return true;
        }

        // Method to validate if the matrix input is well-formed
        private bool IsValidMatrix(IEnumerable<string> matrixInput)
        {
            if (matrixInput == null || !matrixInput.Any()) return false;

            // Ensure all rows have the same length
            int rowLength = matrixInput.First().Length;
            return matrixInput.All(row => row.Length == rowLength);
        }

        // Method to search for words in the matrix
        public IEnumerable<string> Find(IEnumerable<string>? wordstream)
        {
            if (wordstream == null || !wordstream.Any())
            {
                // If the word stream is null or empty, return an empty set of words
                return new List<string>();
            }

            var foundWords = new HashSet<string>();
    
            // Search words in rows and columns
            foreach (var word in wordstream)
            {
                // Ensure word is in uppercase and search in rows
                for (int row = 0; row < _matrix.Count; row++)
                {
                    if (_matrix[row].Contains(word) && !foundWords.Contains(word))
                    {
                        foundWords.Add(word);
                        MarkWordInResultMatrix(word, row, false);  // Mark word horizontally
                    }
                }

                // Search in columns
                for (int col = 0; col < _columns.Count; col++)
                {
                    if (_columns[col].Contains(word) && !foundWords.Contains(word))
                    {
                        foundWords.Add(word);
                        MarkWordInResultMatrix(word, col, true);   // Mark word vertically
                    }
                }
            }

            // Return the found words or an empty list if none are found
            return foundWords.Any() ? foundWords.Take(10) : new List<string>();
        }

        // Method to mark the found word in the result matrix
        private void MarkWordInResultMatrix(string word, int index, bool isVertical)
        {
            if (isVertical)
            {
                int startRow = _columns[index].IndexOf(word, StringComparison.Ordinal);
                for (int i = 0; i < word.Length; i++)
                {
                    _resultMatrix[startRow + i, index] = word[i];
                }
            }
            else
            {
                int startCol = _matrix[index].IndexOf(word, StringComparison.Ordinal);
                for (int i = 0; i < word.Length; i++)
                {
                    _resultMatrix[index, startCol + i] = word[i];
                }
            }
        }
        
        // Method to get the resulting matrix with only found words
        public char[,] GetResultMatrix()
        {
            return _resultMatrix;
        }

        // Method to get the full board matrix
        public char[,] GetBoard()
        {
            return _board;
        }
    }
}