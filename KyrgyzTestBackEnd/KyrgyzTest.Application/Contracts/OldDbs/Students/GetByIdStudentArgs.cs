namespace KyrgyzTest.Application.Contracts.OldDbs.Students;

public class GetByIdStudentArgs
{
    public required int Id { get; set; }
    
    public required string Source { get; set; }
}