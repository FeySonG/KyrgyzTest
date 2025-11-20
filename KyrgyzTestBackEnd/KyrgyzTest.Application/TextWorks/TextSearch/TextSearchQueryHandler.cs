using FuzzySharp;
using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.TextWorks;

namespace KyrgyzTest.Application.TextWorks.TextSearch;

public class TextSearchQueryHandler :  IQueryHandler<TextSearchQuery, List<WordFrequency>>
{
    public Task<List<WordFrequency>> Handle(TextSearchQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Args.Query))
            throw new ArgumentNullException(nameof(request.Args.Query), "Ничего не найденно");
        
        var result = request.Args.Words
            .Where(w => Fuzz.PartialRatio(w.Word, request.Args.Query) > 70)
            .ToList();
        
        return Task.FromResult(result);
    }
}