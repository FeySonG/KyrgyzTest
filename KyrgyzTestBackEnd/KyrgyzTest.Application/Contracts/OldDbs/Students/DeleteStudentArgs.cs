namespace KyrgyzTest.Application.Contracts.OldDbs.Students;

public class DeleteStudentArgs
{
    public required int Id { get; set; }
    
    public required string Source { get; set; }
}