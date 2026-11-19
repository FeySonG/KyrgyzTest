using KyrgyzTest.Application.Contracts.Mailysearchs;
using KyrgyzTest.Application.Contracts.OldDbs.Students;

namespace KyrgyzTest.Application.Contracts.OldDbs.TestResults;

public class TestSearchResponseDto
{
    public List<TestStudentsDto> Students { get; set; } = new List<TestStudentsDto>();
    public List<TestResultSearchDto> TestResults { get; set; } = new List<TestResultSearchDto>();
    
    
}