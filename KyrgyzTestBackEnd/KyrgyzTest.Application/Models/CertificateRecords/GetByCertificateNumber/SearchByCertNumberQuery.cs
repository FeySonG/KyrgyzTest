using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.CertificateRecords;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.CertificateRecords.GetByCertificateNumber;

public record SearchByCertNumberQuery(string CertificateNumber)
    : IQuery<Result<List<CertificateRecordDto>>>;
