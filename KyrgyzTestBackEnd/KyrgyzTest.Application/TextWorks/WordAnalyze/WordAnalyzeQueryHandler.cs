using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.TextWorks.TextAnalysis;
using KyrgyzTest.Application.TextWorks.TextManipulation;
using Xceed.Words.NET;

namespace KyrgyzTest.Application.TextWorks.WordAnalyze;

public class WordAnalyzeQueryHandler : IQueryHandler<WordAnalyzeQuery, TextAnalysisDto>
{
    public Task<TextAnalysisDto> Handle(WordAnalyzeQuery request, CancellationToken cancellationToken)
    {
        if (!request.FileName.EndsWith(".docx"))
            throw new ArgumentException("Поддерживаются только .docx файлы");
        
        string text;

        using (var doc = DocX.Load(request.FileStream))
        {
            text = doc.Text;
        }
        
        var result = FrecuencyAnalyze.Analyze(text);
        
        return Task.FromResult(result);
    }
}