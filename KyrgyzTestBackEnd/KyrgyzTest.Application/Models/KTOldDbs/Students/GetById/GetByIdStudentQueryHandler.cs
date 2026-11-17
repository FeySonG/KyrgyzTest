using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Abstractions.OldDbAbstractions.Regulations;
using KyrgyzTest.Application.Abstractions.OldDbAbstractions.Students;
using KyrgyzTest.Application.Abstractions.OldDbAbstractions.TestResults;
using KyrgyzTest.Application.Abstractions.OldDbRegionAbstractions.Regulations;
using KyrgyzTest.Application.Abstractions.OldDbRegionAbstractions.Students;
using KyrgyzTest.Application.Abstractions.OldDbRegionAbstractions.TestResults;
using KyrgyzTest.Application.Contracts.Mailysearchs;
using KyrgyzTest.Application.Contracts.OldDbs.Regulations;
using KyrgyzTest.Application.Contracts.OldDbs.Students;
using KyrgyzTest.Application.Contracts.OldDbs.TestResults;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.KTOldDbs.Students.GetById;

public class GetByIdStudentQueryHandler(
    IStudentRepository studentRepository,
    IStudentRegionRepository studentRegionRepository,
    ITestResultRepository testResultRepository,
    ITestResultRegionRepository testResultRegionRepository,
    IRegulationRepository regulationRepository,
    IRegulationRegionRepository regulationRegionRepository
    ) : IQueryHandler<GetByIdStudentQuery, Result<StudentDto>>
{
    public async Task<Result<StudentDto>> Handle(GetByIdStudentQuery request, CancellationToken cancellationToken)
    {
        var source = request.Args.Source;

        StudentDto? student;
        
        var results = new List<TestResultDto>();
        
        var regulations = new List<RegulationDto>();
        
        if (source == SearchSources.OldDb)
        {
            student = await studentRepository.GetStudentByIdAsync(request.Args.IdStudent);
            if (student is null)
            {
                return new Error(KtOldDbErrorCode.StudentNotFound, $"Student id: {request.Args.IdStudent} not found");
            }
            results = await testResultRepository.GetByStudentIdAsync(request.Args.IdStudent);
            if (results.Count == 0)
            {
                return new Error(KtOldDbErrorCode.ResultNotFound,
                    $"Operation failed result id: {request.Args.IdStudent} not found");
            }
            
            regulations  = await regulationRepository.GetAll();
            
            student.Results = AddRegulation(regulations, results);
            
        } 
        else if(source == SearchSources.Region)
        {
            student = await studentRegionRepository.GetStudentByIdAsync(request.Args.IdStudent);
            if (student is null)
            {
                return new Error(KtOldDbErrorCode.StudentNotFound, $"Student id: {request.Args.IdStudent} not found");
            }
            
            results = await testResultRegionRepository.GetByStudentIdAsync(request.Args.IdStudent);
            if (results.Count == 0)
            {
                return new Error(KtOldDbErrorCode.ResultNotFound,
                    $"Operation failed result id: {request.Args.IdStudent} not found");
            }
            
            regulations = await regulationRegionRepository.GetAll();
            
            student.Results = AddRegulation(regulations, results);
            
        } else
        {
            return new Error(
                KtOldDbErrorCode.InvalidSource,
                $"Unknown source: {source}");
        }

        return student;
    }
    
    private List<TestResultDto> AddRegulation(List<RegulationDto> regulations, List<TestResultDto> results)
    {
        var regulationDict = regulations
            .Where(r => r.Id != 0)
            .ToDictionary(r => r.Id, r => r.ShRegulation);

        foreach (var test in results)
        {
            if (regulationDict.TryGetValue(test.IdRegulation, out var shReg))
            {
                test.ShRegulation = shReg;
            }
        }
        
        return results;
    }
}