namespace WordFinderAPI.Models
{
    public class WordFinderResponse
    {
        public List<string> WordsFound { get; set; }  // List of found words
        public List<string> Matrix { get; set; }      // Full matrix used
        public List<string> ResultMatrix { get; set; } // Matrix with only the found words
    }
}