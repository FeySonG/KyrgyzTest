namespace KyrgyzTest.Application.Contracts.TextWorks;

public class WordFrequency
{
    public string Word { get; set; } = string.Empty;
    public int Count { get; set; }
}

public class SearchRequest
{
    public string Query { get; set; } = string.Empty;
    public List<WordFrequency> Words { get; set; } = new();
}
