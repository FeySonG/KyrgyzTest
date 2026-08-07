using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.CertificateRecords;
using KyrgyzTest.Application.Extensions.Result;
using KyrgyzTest.Core.Models.CertificateRecords;

namespace KyrgyzTest.Application.Models.CertificateRecords.AddFromExcelFile;

public record AddFromExcelCommand(Stream FileStream, string FileName) : ICommand<Result<List<CertificateRecordDto>>>;
