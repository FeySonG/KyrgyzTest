using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.CertificateRecords;
using KyrgyzTest.Application.Extensions.Result;
using KyrgyzTest.Core.Models.CertificateRecords;

namespace KyrgyzTest.Application.Models.CertificateRecords.GetById;

public record GetByIdCertRecordQuery(long CertId) : IQuery<Result<CertificateRecordDto>>;
