using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Contracts.CertificateRecords;
using KyrgyzTest.Application.Extensions.Result;
using KyrgyzTest.Core.Models.CertificateRecords;

namespace KyrgyzTest.Application.Models.CertificateRecords.GetById;

public class GetByIdCertRecordQueryHandler(
                                            ICertificateRecordRepository certRepository
                                            ) : IQueryHandler<GetByIdCertRecordQuery, Result<CertificateRecordDto>>
{
    public async Task<Result<CertificateRecordDto>> Handle(GetByIdCertRecordQuery request, CancellationToken cancellationToken)
    {
        
        var certRecord = await certRepository.GetByIdAsync(request.CertId);
        
        if (certRecord == null)
        {
            return new Error(CertRecordErrorCode.NotFoundById, $"Certificate record Id{request.CertId} not found");
        }
        
        return CertificateRecordDto.MapDto(certRecord);
    }
}