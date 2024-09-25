namespace WordFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordFinderController : ControllerBase
    {
        [HttpPost]
        public ActionResult<WordFinderResponse> FindWords([FromBody] WordFinderRequest request)
        {
            // Validamos si se proporcionan palabras y un tamaño de matriz válido
            if (request.Words == null || request.Words.Count == 0)
            {
                return BadRequest("Debe proporcionar al menos una palabra.");
            }

            if (request.Matrix == null || request.Matrix.Count == 0)
            {
                return BadRequest("Debe proporcionar una matriz válida.");
            }

            try
            {
                // Creamos una instancia de WordFinder con la matriz proporcionada
                WordFinder finder = new WordFinder(request.Matrix);

                // Ejecutamos la búsqueda de palabras
                var wordsFound = finder.Find(request.Words);

                // Creamos la respuesta con las palabras encontradas
                var response = new WordFinderResponse
                {
                    WordsFound = wordsFound,
                    Matrix = request.Matrix  // Devuelve la matriz recibida en el request
                };

                return Ok(response);  // Retornamos la respuesta con las palabras encontradas y la matriz
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Capturamos cualquier error
            }
        }
    }
}