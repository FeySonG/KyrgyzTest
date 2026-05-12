namespace KyrgyzTest.OldDb.Seeds;

using Meilisearch;
using Microsoft.EntityFrameworkCore;

public class MeiliSearchSeeder(LegacyDbContext dbContext)
{
    private readonly MeilisearchClient _client = new("http://localhost:7700");

    public async Task SeedAsync()
    {
        var index = _client.Index("students");

        // await index.UpdateSearchableAttributesAsync(new[]
        // {
        //     "NameStudent"
        // });

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