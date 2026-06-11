using KyrgyzTest.Application.Abstractions.MeilisearchAbstractions;
using KyrgyzTest.Application.Contracts.Mailysearchs;
using Meilisearch;

namespace KyrgyzTest.DAL.Services.Meilisearchs;

public class MeiliSearchService : ISearchService
{
    private readonly MeilisearchClient _client =
        new("http://localhost:7700");

    public async Task<List<TestResultSearchDto>> SearchAsync(string query)
    {
        var result = await _client
            .Index("testResults")
            .SearchAsync<TestResultSearchDto>(query);

        return result.Hits.ToList();
    }
}