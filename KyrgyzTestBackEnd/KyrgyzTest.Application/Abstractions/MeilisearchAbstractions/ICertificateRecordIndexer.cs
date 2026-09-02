using KyrgyzTest.Application.Contracts.CertificateRecords;

namespace KyrgyzTest.Application.Abstractions.MeilisearchAbstractions;

public interface ICertificateRecordIndexer
{
    Task AddOrUpdateAsync(
        IEnumerable<CertificateRecordDto> records,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(long id, CancellationToken cancellationToken = default);

    Task RebuildAsync(
        IEnumerable<CertificateRecordDto> records,
        CancellationToken cancellationToken = default);
}
