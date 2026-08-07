using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.CertificateRecords;
using KyrgyzTest.Application.Extensions.Result;
using KyrgyzTest.Core.Models.CertificateRecords;

namespace KyrgyzTest.Application.Models.CertificateRecords.GetByCertificateNumber;

public class SearchByCertNumberQueryHandler(
    ICertificateRecordRepository certRepository)
    : IQueryHandler<SearchByCertNumberQuery, Result<List<CertificateRecordDto>>>
{
    public async Task<Result<List<CertificateRecordDto>>> Handle(
        SearchByCertNumberQuery request,
        CancellationToken cancellationToken)
    {
        var records = await certRepository.GetAllByCertNumberAsync(request.CertificateNumber);

        if (records.Count == 0)
        {
            return Result.Fail<List<CertificateRecordDto>>(
                CertRecordErrorCode.NotFound,
                "Сертификаты с указанным номером не найдены.");
        }

        // Если номер продублирован, клиент получит все совпадающие записи.
        return CertificateRecordDto.MapListDto(records);
    }
}
