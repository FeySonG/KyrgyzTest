using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.TextWorks.TextAnalysis;

namespace KyrgyzTest.Application.TextWorks.WordAnalyze;

public record WordAnalyzeQuery(Stream FileStream, string FileName) : IQuery<TextAnalysisDto>;