using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.CertificateRecords;
using KyrgyzTest.Application.Extensions.Result;
using KyrgyzTest.Core.Models.CertificateRecords;

namespace KyrgyzTest.Application.Models.CertificateRecords.GetByDateRange;

public class GetCertificateRecordsByDateRangeQueryHandler(
    ICertificateRecordRepository certificateRecordRepository)
    : IQueryHandler<GetCertificateRecordsByDateRangeQuery, Result<List<CertificateRecordDto>>>
{
    public async Task<Result<List<CertificateRecordDto>>> Handle(
        GetCertificateRecordsByDateRangeQuery request,
        CancellationToken cancellationToken)
    {
        if (request.StartDate > request.EndDate)
        {
            return Result.Fail<List<CertificateRecordDto>>(
                "CertificateRecord.InvalidDateRange",
                "Дата начала диапазона не может быть позже даты окончания.");
        }

        // Конечная дата включается целиком, даже если в IssueDate хранится время.
        var records = await certificateRecordRepository.GetByDateRangeAsync(
            request.StartDate.Date,
            request.EndDate.Date.AddDays(1));

        return CertificateRecordDto.MapListDto(records);
    }
}
