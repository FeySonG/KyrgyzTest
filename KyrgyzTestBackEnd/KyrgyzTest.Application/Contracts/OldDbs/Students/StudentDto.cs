using KyrgyzTest.Application.Contracts.OldDbs.TestResults;

namespace KyrgyzTest.Application.Contracts.OldDbs.Students;

public class StudentDto
{
    public int Id { get; set; }
    
    public int StudentId { get; set; }
    
    public required long Pin { get; set; }
    
    public required long CertificateNumber { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public string? MiddleName { get; set; }
    
    public List<TestResultDto> Results { get; set; } = [];
}