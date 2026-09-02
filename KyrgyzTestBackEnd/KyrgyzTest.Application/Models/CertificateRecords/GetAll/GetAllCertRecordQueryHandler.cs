using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.CertificateRecords;
using KyrgyzTest.Application.Extensions.Result;
using KyrgyzTest.Core.Models.CertificateRecords;

namespace KyrgyzTest.Application.Models.CertificateRecords.GetAll;

public class GetAllCertRecordQueryHandler(
                                           ICertificateRecordRepository certRepository 
                                            )
    
    : IQueryHandler<GetAllCertRecordQuery, Result<List<CertificateRecordDto>>>
{
    public async Task<Result<List<CertificateRecordDto>>> Handle(GetAllCertRecordQuery request, CancellationToken cancellationToken)
    {
        var certRecords = await certRepository.GetAllAsync();
        
        return CertificateRecordDto.MapListDto(certRecords);
    }
}