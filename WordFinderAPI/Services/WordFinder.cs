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
}
