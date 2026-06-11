namespace KyrgyzTest.Application.Contracts.OldDbs.Students;

public class TestStudentsDto
{
    public string SearchStudentId { get; set; } = null!;

    public int StudentId { get; set; }

    public string StudentName { get; set; } = null!;
    
    public required string Source { get; set; }
}