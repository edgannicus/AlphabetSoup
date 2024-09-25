namespace WordFinderAPI.Services
{ 
    public class WordFinder
    {
        private char[,] board;
        private List<string> matrix;
        private List<string> columns;
        private Random random = new Random();
        private int matrixSize;

        // Constructor que recibe una matriz o la genera dinámicamente
        public WordFinder(IEnumerable<string> matrixInput)
        {
            matrix = matrixInput.ToList();
            matrixSize = matrix.Max(row => row.Length);
            
            // Inicializa la matriz con ceros ('0') para poder modificarla
            board = new char[matrix.Count, matrixSize];
            FillBoardWithMatrix();

            // Prepara las columnas para la búsqueda vertical
            columns = new List<string>();
            if (matrix.Count > 0)
            {
                for (int col = 0; col < matrix[0].Length; col++)
                {
                    string column = "";
                    for (int row = 0; row < matrix.Count; row++)
                    {
                        column += board[row, col];
                    }
                    columns.Add(column);
                }
            }
        }

        // Método para buscar palabras en la matriz
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            var foundWords = new HashSet<string>();

            foreach (var word in wordstream)
            {
                // Busca la palabra en filas
                if (matrix.Any(row => row.Contains(word)))
                {
                    foundWords.Add(word);
                }

                // Busca la palabra en columnas
                if (columns.Any(col => col.Contains(word)))
                {
                    foundWords.Add(word);
                }
            }

            return foundWords.Take(10);  // Retorna las primeras 10 palabras encontradas
        }

        // Llena el board con las palabras proporcionadas en la matriz
        private void FillBoardWithMatrix()
        {
            for (int row = 0; row < matrix.Count; row++)
            {
                var matrixRow = matrix[row];
                for (int col = 0; col < matrixRow.Length; col++)
                {
                    board[row, col] = matrixRow[col];
                }
            }

            // Rellenar con letras aleatorias en los espacios vacíos (si hay '0')
            FillBoardWithRandomLetters();
        }

        // Rellena los espacios vacíos con letras aleatorias
        private void FillBoardWithRandomLetters()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == '\0')  // Solo llena los espacios vacíos
                    {
                        board[row, col] = (char)random.Next('A', 'Z' + 1);
                    }
                }
            }
        }

        // Método para mostrar la matriz (para depuración)
        public void DisplayBoard()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    Console.Write(board[row, col] + " ");
                }
                Console.WriteLine();
            }
        }

        // Método para obtener la matriz resultante
        public char[,] GetBoard()
        {
            return board;
        }
    }

    
    /*public class WordFinder
    {
        private char[,] board;
        private Random random = new Random();
        private int matrixSize;

        // Constructor que ajusta el tamaño de la matriz según la palabra más larga
        public WordFinder(List<string> words)
        {
            // Determina el tamaño mínimo de la matriz basado en la longitud de la palabra más larga
            int maxWordLength = words.Max(w => w.Length);
            matrixSize = Math.Max(5, maxWordLength);  // Establece un tamaño mínimo de 5x5

            // Crea la matriz
            board = new char[matrixSize, matrixSize];
            FillBoardWithRandomLetters();  // Llenar con letras aleatorias

            PlaceWordsStrategically(words);  // Colocar palabras estratégicamente
        }

        // Método para colocar las palabras de forma estratégica, incluyendo cruces
        public void PlaceWordsStrategically(List<string> words)
        {
            // Coloca la primera palabra horizontalmente en la primera fila
            PlaceWord(words[0], 0, 1, false);  // Ejemplo de "HELLO"

            // Coloca la segunda palabra horizontalmente en la segunda fila
            PlaceWord(words[1], 1, 0, false);  // Ejemplo de "WORLD"

            // Coloca la tercera palabra debajo de la segunda
            PlaceWord(words[2], 2, 0, false);  // Ejemplo de "TEST"

            // Coloca la cuarta palabra verticalmente intersectando la tercera palabra
            PlaceWord(words[3], 2, 3, true);  // Ejemplo de "YES"
        }

        // Método que verifica si una palabra cabe en la posición solicitada
        private bool CanPlaceWord(string word, int row, int col, bool isVertical)
        {
            if (isVertical)
            {
                if (row + word.Length > matrixSize) return false;

                for (int i = 0; i < word.Length; i++)
                {
                    if (board[row + i, col] != '\0' && board[row + i, col] != word[i])
                        return false;
                }
            }
            else
            {
                if (col + word.Length > matrixSize) return false;

                for (int i = 0; i < word.Length; i++)
                {
                    if (board[row, col + i] != '\0' && board[row, col + i] != word[i])
                        return false;
                }
            }
            return true;
        }

        // Método que coloca la palabra en la matriz
        private void PlaceWord(string word, int row, int col, bool isVertical)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (isVertical)
                {
                    board[row + i, col] = word[i];  // Coloca la palabra verticalmente
                }
                else
                {
                    board[row, col + i] = word[i];  // Coloca la palabra horizontalmente
                }
            }
        }

        // Método para llenar los espacios vacíos de la matriz con letras aleatorias
        private void FillBoardWithRandomLetters()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == '\0')  // Solo llena los espacios vacíos
                    {
                        board[row, col] = (char)random.Next('A', 'Z' + 1);
                    }
                }
            }
        }

        // Método para mostrar la matriz (para depuración)
        public void DisplayBoard()
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    Console.Write(board[row, col] + " ");
                }
                Console.WriteLine();
            }
        }
    }*/
    /*public class WordFinder
    {
        private List<string> matrix;
        private List<string> rows;
        private List<string> columns;
        private const int MaxMatrixSize = 64;
// Constructor que recibe una matriz de strings
        public WordFinder(IEnumerable<string> matrix)
        {
            this.matrix = matrix.ToList();
            columns = new List<string>();

            // Generar columnas para la búsqueda vertical
            if (this.matrix.Count > 0)
            {
                for (int col = 0; col < this.matrix[0].Length; col++)
                {
                    string column = "";
                    for (int row = 0; row < this.matrix.Count; row++)
                    {
                        column += this.matrix[row][col];
                    }
                    columns.Add(column);
                }
            }
        }

        // Método para encontrar palabras dentro de la matriz
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            var foundWords = new HashSet<string>();

            foreach (var word in wordstream)
            {
                // Busca la palabra en filas
                if (matrix.Any(row => row.Contains(word)))
                {
                    foundWords.Add(word);
                }

                // Busca la palabra en columnas
                if (columns.Any(col => col.Contains(word)))
                {
                    foundWords.Add(word);
                }
            }

            return foundWords.Take(10);  // Retornar las primeras 10 palabras encontradas
        }
        // Constructor que inicializa las filas y columnas de la matriz
        /*public WordFinder(int matrixSize)
        {
            this.rows = GenerateRandomMatrix(matrixSize);  // O bien usa una matriz fija si estás en pruebas
            this.columns = new List<string>();

            // Generar las columnas
            if (rows.Count > 0 && rows[0].Length > 0)
            {
                for (int col = 0; col < rows[0].Length; col++)
                {
                    string column = "";
                    for (int row = 0; row < rows.Count; row++)
                    {
                        column += rows[row][col];
                    }
                    columns.Add(column);
                }
            }
        }#1#
        /*public WordFinder(int matrixSize)
        {
            // Validación de tamaño
            if (matrixSize <= 0 || matrixSize > 64)
            {
                throw new ArgumentException("El tamaño de la matriz debe ser mayor que 0 y no mayor que 64.");
            }

            this.rows = GenerateRandomMatrix(matrixSize);
            this.columns = new List<string>();

            // Generar columnas si la matriz es válida
            if (rows.Count > 0 && rows[0].Length > 0)
            {
                for (int col = 0; col < rows[0].Length; col++)
                {
                    string column = "";
                    for (int row = 0; row < rows.Count; row++)
                    {
                        column += rows[row][col];
                    }
                    columns.Add(column);
                }
            }
        }#1#
        /*public WordFinder(int matrixSize)
        {
            if (matrixSize <= 0)
            {
                throw new ArgumentException("El tamaño de la matriz debe ser mayor que 0.");
            }

            this.rows = GenerateRandomMatrix(matrixSize);
            this.columns = new List<string>();

            // Solo genera las columnas si hay filas en la matriz
            if (rows.Count > 0 && rows[0].Length > 0)
            {
                for (int col = 0; col < rows[0].Length; col++)
                {
                    string column = "";
                    for (int row = 0; row < rows.Count; row++)
                    {
                        column += rows[row][col];
                    }
                    columns.Add(column);
                }
            }
        }#1#
        /*public WordFinder(int matrixSize)
        {
            this.rows = GenerateRandomMatrix(matrixSize);
            this.columns = new List<string>();

            // Preparamos las columnas solo una vez en el constructor
            for (int col = 0; col < rows[0].Length; col++)
            {
                string column = "";
                for (int row = 0; row < rows.Count; row++)
                {
                    column += rows[row][col];
                }
                columns.Add(column);
            }
        }#1#
        /*public WordFinder(int matrixSize)
        {
            this.rows = matrixSize == 0 ? GenerateRandomMatrix(MaxMatrixSize) : GenerateRandomMatrix(matrixSize);
            this.columns = new List<string>();

            // Preparamos las columnas solo una vez
            for (int col = 0; col < rows[0].Length; col++)
            {
                string column = "";
                for (int row = 0; row < rows.Count; row++)
                {
                    column += rows[row][col];
                }
                columns.Add(column);
            }
        }#1#

        // Función para generar una matriz de letras aleatorias
        private List<string> GenerateRandomMatrix(int size)
        {
            Random random = new Random();
            List<string> matrix = new List<string>();

            for (int i = 0; i < size; i++)
            {
                char[] row = new char[size];
                for (int j = 0; j < size; j++)
                {
                    row[j] = (char)random.Next('a', 'z' + 1);  // Genera una letra aleatoria
                }
                matrix.Add(new string(row));
            }

            return matrix;
        }
        
        // Método para buscar palabras en la matriz
        /*public IEnumerable<string> Find(IEnumerable<string> wordStream)
        {
            // Si la matriz está vacía, retornamos un conjunto vacío
            if (rows == null || rows.Count == 0)
            {
                return Enumerable.Empty<string>();
            }

            var wordList = wordStream.Distinct().ToList();
            var foundWords = new HashSet<string>();

            foreach (var word in wordList)
            {
                if (IsWordInMatrix(word))
                {
                    foundWords.Add(word);
                }
            }

            return foundWords.Take(10);
        }#1#
        /*public IEnumerable<string> Find(IEnumerable<string> wordStream)
        {
            var wordList = wordStream.Distinct().ToList();  // Elimina duplicados
            var foundWords = new HashSet<string>();  // Evita palabras repetidas en el resultado

            foreach (var word in wordList)
            {
                if (IsWordInMatrix(word))
                {
                    foundWords.Add(word);
                }
            }

            return foundWords;
        }#1#
        private bool IsWordInMatrix(string word)
        {
            // Verifica en filas
            foreach (var row in rows)
            {
                if (row.Contains(word))
                {
                    Console.WriteLine($"Palabra {word} encontrada en fila: {row}");
                    return true;
                }
            }

            // Verifica en columnas
            foreach (var col in columns)
            {
                if (col.Contains(word))
                {
                    Console.WriteLine($"Palabra {word} encontrada en columna: {col}");
                    return true;
                }
            }

            return false;
        }
        /*private bool IsWordInMatrix(string word)
        {
            // Verifica en filas
            foreach (var row in rows)
            {
                if (row.Contains(word))
                {
                    return true;
                }
            }

            // Verifica en columnas
            foreach (var col in columns)
            {
                if (col.Contains(word))
                {
                    return true;
                }
            }

            return false;
        }#1#
        /*public IEnumerable<string> Find(IEnumerable<string> wordStream)
        {
            var wordList = wordStream.Distinct().Take(4).ToList();
            var foundWords = new Dictionary<string, int>();

            foreach (var word in wordList)
            {
                if (IsWordInMatrix(word))
                {
                    if (foundWords.ContainsKey(word))
                    {
                        foundWords[word]++;
                    }
                    else
                    {
                        foundWords[word] = 1;
                    }
                }
            }

            return foundWords
                .OrderByDescending(kvp => kvp.Value)
                .Take(10)
                .Select(kvp => kvp.Key);
        }#1#

        // Método para verificar si una palabra está en las filas o columnas
        /*private bool IsWordInMatrix(string word)
        {
            // Verifica horizontalmente en las filas
            foreach (var row in rows)
            {
                if (row.Contains(word))
                {
                    return true;
                }
            }

            // Verifica verticalmente en las columnas
            foreach (var col in columns)
            {
                if (col.Contains(word))
                {
                    return true;
                }
            }

            return false;
        }#1#
        /*private bool IsWordInMatrix(string word)
        {
            // Verifica horizontalmente en las filas
            foreach (var row in rows)
            {
                if (row.Contains(word))
                {
                    return true;
                }
            }

            // Verifica verticalmente en las columnas
            foreach (var col in columns)
            {
                if (col.Contains(word))
                {
                    return true;
                }
            }

            return false;
        }#1#
        /*private bool IsWordInMatrix(string word)
        {
            if (rows.Any(row => row.Contains(word)))
            {
                return true;
            }

            if (columns.Any(col => col.Contains(word)))
            {
                return true;
            }

            return false;
        }#1#
        public IEnumerable<string> GetMatrix()
        {
            return rows;
        }
    }*/
}
