using Meilisearch;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDbRegion.Seeds;

public class MeiliSearchSeeder(LegacyDbRegionContext dbRegionContext)
{
    private readonly MeilisearchClient _client = new("http://localhost:7700");

    public async Task SeedAsync()
    {
        var index = _client.Index("testResultsRegion");

        var batchSize = 1000;
        var total = await dbRegionContext.TestResults.CountAsync();

        for (int i = 0; i < total; i += batchSize)
        {
            var batch = await dbRegionContext.TestResults
                .AsNoTracking()
                .Skip(i)
                .Take(batchSize)
                .ToListAsync();

            await index.AddDocumentsAsync(batch);
        }
    }
}