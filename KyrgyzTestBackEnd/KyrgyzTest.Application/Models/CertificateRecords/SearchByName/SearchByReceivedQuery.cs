using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.CertificateRecords;
using KyrgyzTest.Application.Contracts.Mailysearchs;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.CertificateRecords.SearchByName;

public record SearchByReceivedQuery(CertRecordSearchDto Args) : IQuery<Result<List<CertificateRecordDto>>>;
