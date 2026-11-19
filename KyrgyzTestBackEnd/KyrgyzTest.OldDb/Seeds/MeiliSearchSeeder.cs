namespace KyrgyzTest.OldDb.Seeds;

using Meilisearch;
using Microsoft.EntityFrameworkCore;

public class MeiliSearchSeeder(LegacyDbContext dbContext, MeilisearchClient client)
{
    private readonly MeilisearchClient _client = client;

    public async Task SeedAsync()
    {
        var index = _client.Index("testResults");

        var batchSize = 1000;
        var total = await dbContext.TestResults.CountAsync();

        for (int i = 0; i < total; i += batchSize)
        {
            var batch = await dbContext.TestResults
                .AsNoTracking()
                .Skip(i)
                .Take(batchSize)
                .ToListAsync();

            await index.AddDocumentsAsync(batch);
        }
    }
}
