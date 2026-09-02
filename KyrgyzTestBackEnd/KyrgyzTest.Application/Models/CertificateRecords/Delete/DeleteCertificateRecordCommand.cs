using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.CertificateRecords;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.CertificateRecords.Delete;

public record DeleteCertificateRecordCommand(long Id)
    : ICommand<Result<CertificateRecordDto>>;
