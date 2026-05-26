using KyrgyzTest.Application.Abstractions.MeilisearchAbstractions;
using KyrgyzTest.Application.Contracts.OldDbs.TestResults;
using Meilisearch;

namespace KyrgyzTest.OldDbRegion.Repositories.Meilisearchs;

public class MeiliSearchService : IRegionSearchService
{
    private readonly MeilisearchClient _client;

    public MeiliSearchService()
    {
        _client = new MeilisearchClient("http://localhost:7700");
    }

    public async Task<List<TestResultDto>> RegionSearchAsync(string query)
    {
        var result = await _client
            .Index("testResultsRegion")
            .SearchAsync<TestResultDto>(query);

        return result.Hits.ToList();
    }

}