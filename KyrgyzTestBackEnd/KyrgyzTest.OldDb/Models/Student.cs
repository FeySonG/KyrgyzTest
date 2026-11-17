using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KyrgyzTest.OldDb.Models;

public class Student 
{
    
    public int Id { get; set; }
    
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