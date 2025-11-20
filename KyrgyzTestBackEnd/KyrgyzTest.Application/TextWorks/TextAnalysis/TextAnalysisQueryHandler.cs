using System.Text.RegularExpressions;
using KyrgyzTest.Application.Extensions.Result;
using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.TextWorks.TextAnalysis;
using KyrgyzTest.Application.TextWorks.TextManipulation;

namespace KyrgyzTest.Application.TextWorks.TextAnalysis;

public class TextAnalysisQueryHandler : IQueryHandler<TextAnalysisQuery, TextAnalysisDto>
{
    public Task<TextAnalysisDto> Handle(TextAnalysisQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
            throw new ArgumentException("Текст не может быть пустым");

        var result = FrecuencyAnalyze.Analyze(request.Text);

        return Task.FromResult(result);
    }
}