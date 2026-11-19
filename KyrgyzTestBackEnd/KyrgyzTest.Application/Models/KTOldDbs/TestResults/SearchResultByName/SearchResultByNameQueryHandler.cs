using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Abstractions.MeilisearchAbstractions;
using KyrgyzTest.Application.Contracts.Mailysearchs;
using KyrgyzTest.Application.Contracts.OldDbs.Students;
using KyrgyzTest.Application.Contracts.OldDbs.TestResults;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.KTOldDbs.TestResults.SearchResultByName;

public class SearchResultByNameQueryHandler(
    ISearchService searchService)
    : IQueryHandler<SearchResultByNameQuery, Result<TestSearchResponseDto>>
{
    public async Task<Result<TestSearchResponseDto>> Handle(
        SearchResultByNameQuery request,
        CancellationToken cancellationToken)
    {
        var name = request.Args.Name;

        if (string.IsNullOrWhiteSpace(name))
            return new Error(
                KtOldDbErrorCode.ResultNotFound,
                $"Invalid data to search: '{name}'");

        var searchResult = 
                        await searchService.SearchAsync<TestResultSearchDto>(
                                                            MeiliIndexes.TestResults,
                                                                name );

        return new TestSearchResponseDto
        {
            Students = GetStudentNames(searchResult),
            TestResults = searchResult
        };
    }

    private List<TestStudentsDto> GetStudentNames(
        List<TestResultSearchDto> results)
    {
        return results
            .DistinctBy(x => $"{x.Source}_{x.IdStudent}")
            .Select(x => new TestStudentsDto
            {
                SearchStudentId = $"{x.Source}_{x.IdStudent}",
                Source = x.Source,
                StudentId = x.IdStudent,
                StudentName = x.NameStudent
            })
            .OrderBy(x => x.StudentName)
            .ToList();
    }
}