namespace WordFinderAPI.Models
{
    public class WordFinderRequest
    {
        public List<string> Words { get; set; }  // Las palabras que se buscan
        public List<string> Matrix { get; set; } // La matriz donde se buscarán las palabras
    }
}