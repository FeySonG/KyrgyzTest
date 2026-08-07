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
        if (entities.Value is null)
        {
            return new List<CertificateRecordDto>();
        }
        
        await certRecordRepository.AddRangeAsync(entities.Value);
        await unitOfWork.SaveChangesAsync();

        var certificateRecords = CertificateRecordDto.MapListDto(entities.Value);
        await certificateRecordIndexer.AddOrUpdateAsync(certificateRecords, cancellationToken);

        return certificateRecords;
    }
    
}
