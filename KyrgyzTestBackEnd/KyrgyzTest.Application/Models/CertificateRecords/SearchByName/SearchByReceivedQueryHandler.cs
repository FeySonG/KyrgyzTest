using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Abstractions.MeilisearchAbstractions;
using KyrgyzTest.Application.Contracts.CertificateRecords;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.CertificateRecords.SearchByName;

public class SearchByReceivedQueryHandler(
    ISearchService searchService)
    : IQueryHandler<SearchByReceivedQuery, Result<List<CertificateRecordDto>>>
{
    public async Task<Result<List<CertificateRecordDto>>> Handle(
        SearchByReceivedQuery request,
        CancellationToken cancellationToken)
    {
        var records = await searchService.SearchAsync<CertificateRecordDto>(
            MeiliIndexes.CertificateRecords,
            request.Args.Name);

        return records;
    }
}
