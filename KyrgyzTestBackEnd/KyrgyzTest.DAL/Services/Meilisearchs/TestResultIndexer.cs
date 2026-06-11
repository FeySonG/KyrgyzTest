using KyrgyzTest.Application.Abstractions.OldDbAbstractions.TestResults;
using KyrgyzTest.Application.Abstractions.OldDbRegionAbstractions.TestResults;
using KyrgyzTest.Application.Contracts.Mailysearchs;
using Meilisearch;

namespace KyrgyzTest.DAL.Services.Meilisearchs;

public class TestResultIndexer
{
    private readonly ITestResultRepository _oldRepo;
    private readonly ITestResultRegionRepository _regionRepo;

    private readonly MeilisearchClient _client =
        new("http://localhost:7700");

    public TestResultIndexer(
        ITestResultRepository oldRepo,
        ITestResultRegionRepository regionRepo)
    {
        _oldRepo = oldRepo;
        _regionRepo = regionRepo;
    }

    public async Task RebuildAsync()
    {
        var oldResults = await _oldRepo.GetAllAsync();
        var regionResults = await _regionRepo.GetAllAsync();

        var documents = new List<TestResultSearchDto>();

        documents.AddRange(
            oldResults.Select(x => new TestResultSearchDto
            {
                SearchId = $"old_{x.Id}",
                Source = SearchSources.OldDb,

                Id = x.Id,
                IdStudent = x.IdStudent,
                NameStudent = x.NameStudent,
                NameGroup = x.NameGroup,
                NameFacultet = x.NameFacultet,
                NameDiscipline = x.NameDiscipline,
                GenerateDate = x.GenerateDate
            }));

        documents.AddRange(
            regionResults.Select(x => new TestResultSearchDto
            {
                SearchId = $"region_{x.Id}",
                Source = SearchSources.Region,

                Id = x.Id,
                IdStudent = x.IdStudent,
                NameStudent = x.NameStudent,
                NameGroup = x.NameGroup,
                NameFacultet = x.NameFacultet,
                NameDiscipline = x.NameDiscipline,
                GenerateDate = x.GenerateDate
            }));

        var index = _client.Index("testResults");

        await index.DeleteAllDocumentsAsync();

        await index.AddDocumentsAsync(
            documents,
            "searchId");
    }
}