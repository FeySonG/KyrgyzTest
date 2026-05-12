using KyrgyzTest.Application.Contracts.OldDbs.TestResults;

namespace KyrgyzTest.Application.Abstractions.MeilisearchAbstractions;

public interface ISearchService
{
    Task<List<TestResultDto>> SearchAsync(string query);
}