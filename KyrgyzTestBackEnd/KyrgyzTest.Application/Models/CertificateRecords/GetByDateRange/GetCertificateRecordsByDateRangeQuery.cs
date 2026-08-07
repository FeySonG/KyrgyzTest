using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.CertificateRecords;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.CertificateRecords.GetByDateRange;

public record GetCertificateRecordsByDateRangeQuery(DateTime StartDate, DateTime EndDate)
    : IQuery<Result<List<CertificateRecordDto>>>;
