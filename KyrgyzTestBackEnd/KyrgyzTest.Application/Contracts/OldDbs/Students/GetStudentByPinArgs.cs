namespace KyrgyzTest.Application.Contracts.OldDbs.Students;

public class GetStudentByPinArgs
{
    public required string Source { get; set; }
    
    public required long Pin { get; set; }
}