using KyrgyzTest.Application.Contracts.OldDbs.TestResults;

namespace KyrgyzTest.Application.Abstractions.MeilisearchAbstractions;

public interface IRegionSearchService
{
    Task<List<TestResultDto>> RegionSearchAsync(string query);

}