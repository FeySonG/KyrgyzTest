using KyrgyzTest.Application.Extensions.Result;
using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.TextWorks.TextAnalysis;

namespace KyrgyzTest.Application.TextWorks.TextAnalysis;

public record TextAnalysisQuery(string Text) : IQuery<TextAnalysisDto>;
