namespace KyrgyzTest.Application.Contracts.OldDbs.Students;

public class GetByStudentIdArgs
{
    public required int IdStudent { get; set; }
    
    public required string Source { get; set; }
}