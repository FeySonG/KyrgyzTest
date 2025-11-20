namespace KyrgyzTest.Application.Contracts.TextWorks.TextAnalysis;

public class TextAnalysisDto
{
    public long WordCount { get; set; }
    
    public long CharCount { get; set; }

    public List<string> Words { get; set; } = new();
    
    public Dictionary<string, int> Frequency { get; set; } = new();
}