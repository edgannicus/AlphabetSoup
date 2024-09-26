namespace WordFinderAPI.Services;

public class WordLocation
{
    public string Word { get; set; }
    public (int Row, int Col) StartPosition { get; set; }
    public (int Row, int Col) EndPosition { get; set; }
    public string Direction { get; set; }

    public WordLocation(string word, (int, int) startPosition, (int, int) endPosition, string direction)
    {
        Word = word;
        StartPosition = startPosition;
        EndPosition = endPosition;
        Direction = direction;
    }
}