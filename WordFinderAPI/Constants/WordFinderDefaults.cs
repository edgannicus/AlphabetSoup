namespace WordFinderAPI.Constants
{
    public static class WordFinderDefaults
    {
        public const int MaxMatrixSize = 64;
        // Default matrix if none is provided
        public static readonly List<string> DefaultMatrix = new List<string>
        {
            "TEST", 
            "BDFG",
            "UXXX",
            "GXXX"
        };

        // Default words to search if none are provided
        public static readonly List<string> DefaultWords = new List<string>
        {
            "TEST",
            "BUG"
        };
    }
}