using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.CertificateRecords;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.CertificateRecords.GetAll;

public record GetAllCertRecordQuery() : IQuery<Result<List<CertificateRecordDto>>>;
