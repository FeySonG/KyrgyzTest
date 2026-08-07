using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Abstractions.MeilisearchAbstractions;
using KyrgyzTest.Application.Contracts.CertificateRecords;
using KyrgyzTest.Application.Extensions.Result;
using KyrgyzTest.Application.Services;
using KyrgyzTest.Core.Models.CertificateRecords;

namespace KyrgyzTest.Application.Models.CertificateRecords.Delete;

public class DeleteCertificateRecordCommandHandler(
    ICertificateRecordRepository certificateRecordRepository,
    IUnitOfWork unitOfWork,
    ICertificateRecordIndexer certificateRecordIndexer)
    : ICommandHandler<DeleteCertificateRecordCommand, Result<CertificateRecordDto>>
{
    public async Task<Result<CertificateRecordDto>> Handle(
        DeleteCertificateRecordCommand request,
        CancellationToken cancellationToken)
    {
        var record = await certificateRecordRepository.GetByIdAsync(request.Id);
        if (record is null)
        {
            return Result.Fail<CertificateRecordDto>(
                CertRecordErrorCode.NotFoundById,
                "Запись сертификата не найдена.");
        }

        certificateRecordRepository.Remove(record);
        await unitOfWork.SaveChangesAsync();

        // После удаления из основной БД удаляем документ и из поискового индекса.
        await certificateRecordIndexer.DeleteAsync(record.Id, cancellationToken);

        return CertificateRecordDto.MapDto(record);
    }
}
