namespace KyrgyzTest.Application.Contracts.Mailysearchs;

public class TestResultSearchDto
{
    public string SearchId { get; set; } = null!;

    public string Source { get; set; } = null!;

    public int Id { get; set; }

    public int IdStudent { get; set; }

    public string NameStudent { get; set; } = null!;

    public string NameGroup { get; set; } = null!;

    public string NameFacultet { get; set; } = null!;

    public string NameDiscipline { get; set; } = null!;

    public DateTime GenerateDate { get; set; }
}