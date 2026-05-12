using KyrgyzTest.Application.Abstractions.MeilisearchAbstractions;
using KyrgyzTest.Application.Contracts.OldDbs.TestResults;
using Meilisearch;

namespace KyrgyzTest.OldDb.Repositories.Meilisearchs;

public class MeiliSearchService : ISearchService
{
    private readonly MeilisearchClient _client;

    public MeiliSearchService()
    {
        _client = new MeilisearchClient("http://localhost:7700");
    }

    public async Task<List<TestResultDto>> SearchAsync(string query)
    {
        var result = await _client
            .Index("students")
            .SearchAsync<TestResultDto>(query);

        return result.Hits.ToList();
    }
}