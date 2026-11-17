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

namespace KyrgyzTest.Application.Models.KTOldDbs.Students.GetByPin;

public class GetByPinStudentQueryHandler(
    IStudentRepository studentRepository,
    IStudentRegionRepository studentRegionRepository,
    ITestResultRepository testResultRepository,
    ITestResultRegionRepository testResultRegionRepository,
    IRegulationRepository regulationRepository,
    IRegulationRegionRepository regulationRegionRepository
    ) : IQueryHandler<GetByPinStudentQuery, Result<StudentDto>>
{
    public async Task<Result<StudentDto>> Handle(GetByPinStudentQuery request, CancellationToken cancellationToken)
    {
        var source = request.Args.Source;

        StudentDto? student;
        
        var results = new List<TestResultDto>();
        
        var regulations = new List<RegulationDto>();
        
        if (source == SearchSources.OldDb)
        {
            student = await studentRepository.GetStudentByPin(request.Args.Pin);
            if (student is null)
            {
                return new Error(KtOldDbErrorCode.StudentNotFound, $"Student Pin: {request.Args.Pin} not found");
            }
            
            results = await testResultRepository.GetByStudentIdAsync(student.StudentId);
            if (results.Count == 0)
            {
                return new Error(KtOldDbErrorCode.ResultNotFound,
                    $"Operation failed result id: {student.StudentId} not found");
            }
            
            regulations  = await regulationRepository.GetAll();
            
            student.Results = AddRegulation(regulations, results);
            
        } else if(source == SearchSources.Region)
        {
            student = await studentRegionRepository.GetStudentByPin(request.Args.Pin);
            if (student is null)
            {
                return new Error(KtOldDbErrorCode.StudentNotFound, $"Student Pin: {request.Args.Pin} not found");
            }
            
            results = await testResultRegionRepository.GetByStudentIdAsync(student.StudentId);
            if (results.Count == 0)
            {
                return new Error(KtOldDbErrorCode.ResultNotFound,
                    $"Operation failed result id: {student.StudentId} not found");
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