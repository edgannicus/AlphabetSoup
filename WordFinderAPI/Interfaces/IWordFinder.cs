namespace WordFinderAPI.Interfaces;

public interface IWordFinder
{
    IEnumerable<string> Find(IEnumerable<string> wordstream);
}