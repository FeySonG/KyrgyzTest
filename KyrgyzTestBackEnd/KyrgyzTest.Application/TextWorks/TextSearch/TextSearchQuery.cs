using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.TextWorks;

namespace KyrgyzTest.Application.TextWorks.TextSearch;

public record TextSearchQuery(SearchRequest Args) : IQuery<List<WordFrequency>>; 
