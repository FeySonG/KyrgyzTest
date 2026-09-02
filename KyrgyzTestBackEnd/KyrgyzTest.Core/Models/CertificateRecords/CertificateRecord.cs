using System.Runtime.InteropServices.JavaScript;
using KyrgyzTest.Core.Abstractions;

namespace KyrgyzTest.Core.Models.CertificateRecords;

public class CertificateRecord : Entity
{
    public required string Received { get; set; }
    
    public string? Organization { get; set; }

    public required string CertificateNumber { get; set; }
    
    public string? Level { get; set; }
    
    public DateTime IssueDate { get; set; }
    
    public string? AdditionalInfo { get; set; }

}