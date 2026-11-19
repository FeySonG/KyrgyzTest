using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.OldDbs.TestResults;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.KTOldDbs.TestResults.SearchResultByName;

public record SearchResultByNameQuery(ResultSearchArgs Args) : IQuery<Result<TestSearchResponseDto>>;