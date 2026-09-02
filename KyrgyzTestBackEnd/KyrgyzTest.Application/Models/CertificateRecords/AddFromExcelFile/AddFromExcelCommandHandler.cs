using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Abstractions.MeilisearchAbstractions;
using KyrgyzTest.Application.Contracts.CertificateRecords;
using KyrgyzTest.Application.Extensions.Result;
using KyrgyzTest.Application.Services;
using KyrgyzTest.Core.Models.CertificateRecords;

namespace KyrgyzTest.Application.Models.CertificateRecords.AddFromExcelFile;

public class AddFromExcelCommandHandler(
                  ICertificateRecordRepository certRecordRepository,
                  IUnitOfWork unitOfWork,
                  ICertificateRecordIndexer certificateRecordIndexer
                                            ) : ICommandHandler<AddFromExcelCommand, Result<List<CertificateRecordDto>>>
{
    public async Task<Result<List<CertificateRecordDto>>> Handle(AddFromExcelCommand request, CancellationToken cancellationToken)
    {
        if (!Path.GetExtension(request.FileName)
                .Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
        {
            return new Error(CertRecordErrorCode.UnsupportedFormat, "Поддерживаются только файлы .xlsx"); 
        }
        
        if (request.FileStream.Length == 0)
        {
            return new Error(CertRecordErrorCode.EmptyFile  ,"Файл пуст.");
        }
        
        var reader = new ExcelReader();
        
        var entities = reader.Read(request.FileStream);
        if (entities.IsFailure)
        {
            return entities.Error!;
        }

        var records = entities.Value;
        if (records is null)
        {
            return Result.Fail<List<CertificateRecordDto>>(
                "CertificateRecord.ReadError",
                "Не удалось прочитать записи из Excel-файла.");
        }
        
        await certRecordRepository.AddRangeAsync(records);
        await unitOfWork.SaveChangesAsync();

        var certificateRecords = CertificateRecordDto.MapListDto(records);
        await certificateRecordIndexer.AddOrUpdateAsync(certificateRecords, cancellationToken);

        return certificateRecords;
    }
    
}
