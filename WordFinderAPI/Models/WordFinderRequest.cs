namespace WordFinderAPI.Models
{
    public class WordFinderRequest
    {
        public List<string> Matrix { get; set; }  // This will hold the character matrix
        public List<string> WordStream { get; set; }  // This will hold the words to find
    }
}