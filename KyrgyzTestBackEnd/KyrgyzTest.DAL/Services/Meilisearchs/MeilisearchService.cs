using KyrgyzTest.Application.Abstractions.MeilisearchAbstractions;
using KyrgyzTest.Application.Contracts.Mailysearchs;
using Meilisearch;

namespace KyrgyzTest.DAL.Services.Meilisearchs;

public class MeiliSearchService : ISearchService
{
    private readonly MeilisearchClient _client =
        new("http://localhost:7700");

    public async Task<List<T>> SearchAsync<T>(
        string indexName,
        string query)
    {
        var result = await _client
            .Index(indexName)
            .SearchAsync<T>(query);

        return result.Hits.ToList();
    }
}