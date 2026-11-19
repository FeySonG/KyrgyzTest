using KyrgyzTest.Application.Abstractions.OldDbRegionAbstractions.TestResults;
using KyrgyzTest.Application.Contracts.OldDbs.TestResults;
using KyrgyzTest.OldDbRegion.Models;
using Microsoft.EntityFrameworkCore;

namespace KyrgyzTest.OldDbRegion.Repositories.TestResultRepository;

public class TestResultRepository(LegacyDbRegionContext dbRegionContext) : ITestResultRegionRepository
{
    public async Task<List<TestResultDto>> GetAllAsync()
    {
        var result = await dbRegionContext.TestResults
            .AsNoTracking()
            .ToListAsync();

        return MapListResultDto(result);
    }

    public async Task<TestResultDto?> GetByIdAsync(int id)
    {
        var response =  await dbRegionContext.TestResults.FirstOrDefaultAsync(e => e.Id == id);
        if (response is null) return null;
        
        return MapResultDto(response);
    }

    public async Task<List<TestResultDto>> GetByStudentIdAsync(int studentId)
    {
        var response = await dbRegionContext.TestResults.Where(e => e.IdStudent == studentId).ToListAsync();
        return MapListResultDto(response);
    }
   

    public async Task<List<TestResultDto>> SearchCandidatesAsync(string query)
    {
        var response = await dbRegionContext.TestResults
            .AsNoTracking()
            .Where(x => x.NameStudent.Contains(query)) // грубый фильтр
            .Take(500) // ограничение!
            .ToListAsync();

        return MapListResultDto(response);
    }


    private List<TestResultDto> MapListResultDto(List<TestResult> result)
    {
        var listResult = new List<TestResultDto>();
        
        foreach (var item in result)
        {
            listResult.Add(MapResultDto(item));
        }
        
        return listResult;
    }
    
    private TestResultDto MapResultDto(TestResult result)
    {
        return new TestResultDto
        {
            Id = result.Id,
            IdTest = result.IdTest,
            IdFacultet = result.IdFacultet,
            NameFacultet = result.NameFacultet,
            IdSemestr = result.IdSemestr,
            NameSemestr = result.NameSemestr,
            IdGroup = result.IdGroup,
            NameGroup = result.NameGroup,
            IdStudent = result.IdStudent,
            NameStudent = result.NameStudent,
            IdDiscipline = result.IdDiscipline,
            NameDiscipline = result.NameDiscipline,
            IdExamination = result.IdExamination,
            NameExamination = result.NameExamination,
            IdRegulation = result.IdRegulation,
            TestType = result.TestType,
            FormatTestType = result.FormatTestType,
            Attempt = result.Attempt,
            RightAnswers = result.RightAnsvers,
            Ball = result.Ball,
            BallForTextAnswers = result.BallForTextAnswers,
            BallForAudioAnswers = result.BallForAudioAnswers,
            GenerateDate = result.GenerateDate,
            DeliveryDate = result.DeliveryDate,
            IsHandled = result.IsHandled,
            CountQuest = result.CountQuest,
        };
    }
}