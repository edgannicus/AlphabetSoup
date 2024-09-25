namespace WordFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordFinderController : ControllerBase
    {
        [HttpPost]
        public ActionResult<WordFinderResponse> FindWords([FromBody] WordFinderRequest request)
        {
            // Validate if the matrix and words are provided, if not, defaults will be used
            try
            {
                // Instantiate WordFinder, defaults will be handled inside the class
                WordFinder finder = new WordFinder(request.Matrix);

                // Perform the word search
                var wordsFound = finder.Find(request.Words);

                // Get the matrices
                var fullMatrix = ConvertMatrixToStringList(finder.GetBoard());
                var resultMatrix = ConvertMatrixToStringList(finder.GetResultMatrix());

                // Create the response with the found words and both matrices
                var response = new WordFinderResponse
                {
                    WordsFound = wordsFound.ToList(),
                    Matrix = fullMatrix,
                    ResultMatrix = resultMatrix // Matrix with only found words
                };

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Capture any errors
            }
        }

        // Helper method to convert a matrix of chars into a list of strings
        private List<string> ConvertMatrixToStringList(char[,] matrix)
        {
            var result = new List<string>();

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                string rowString = "";
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    rowString += matrix[row, col];
                }
                result.Add(rowString);
            }

            return result;
        }
    }
}