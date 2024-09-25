namespace WordFinderAPI.Models
{
    public class WordFinderResponse
    {
        public IEnumerable<string> WordsFound { get; set; }  // Las palabras encontradas
        public List<string> Matrix { get; set; }             // La matriz utilizada en la búsqueda
    }
}