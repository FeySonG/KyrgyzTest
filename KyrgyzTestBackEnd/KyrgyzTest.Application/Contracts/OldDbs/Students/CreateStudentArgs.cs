using System.ComponentModel.DataAnnotations;

namespace KyrgyzTest.Application.Contracts.OldDbs.Students;

public class CreateStudentArgs
{
    public required string Source { get; set; }
    
    public required int StudentId { get; set; }
    
    public required long Pin { get; set; }
    
    public required long CertificateNumber { get; set; }
    
    [MaxLength(200)]
    public required string FirstName { get; set; }
    
    [MaxLength(200)]
    public required string LastName { get; set; }
    
    [MaxLength(200)]
    public string? MiddleName { get; set; }
}