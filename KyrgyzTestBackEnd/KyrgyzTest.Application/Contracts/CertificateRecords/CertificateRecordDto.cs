using KyrgyzTest.Core.Models.CertificateRecords;

namespace KyrgyzTest.Application.Contracts.CertificateRecords;

public class CertificateRecordDto
{
    public long Id { get; set; }
    
    public required string Received { get; set; }
    
    public string? Organization { get; set; }

    public string CertificateNumber { get; set; } = null!;
    
    public string? Level { get; set; }
    
    public DateTime IssueDate { get; set; }
    
    public string? AdditionalInfo { get; set; }
    
    
    public static List<CertificateRecordDto> MapListDto(List<CertificateRecord> records)
    {
        var recordDtos = new List<CertificateRecordDto>();
        foreach (CertificateRecord record in records)
        {
            recordDtos.Add(new CertificateRecordDto
            {
                Id = record.Id,
                Received = record.Received,
                Organization = record.Organization,
                CertificateNumber = record.CertificateNumber,
                Level = record.Level,
                IssueDate = record.IssueDate,
                AdditionalInfo = record.AdditionalInfo
            });
        }
        return recordDtos;
    }

    public static CertificateRecordDto MapDto(CertificateRecord record)
    {
        return new CertificateRecordDto
        {
            Id = record.Id,
            Received = record.Received,
            Organization = record.Organization,
            CertificateNumber = record.CertificateNumber,
            Level = record.Level,
            IssueDate = record.IssueDate,
            AdditionalInfo = record.AdditionalInfo  
        };
    }
}