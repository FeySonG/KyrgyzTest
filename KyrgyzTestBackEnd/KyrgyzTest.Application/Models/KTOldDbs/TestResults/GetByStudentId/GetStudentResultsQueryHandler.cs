using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Abstractions.OldDbAbstractions.Regulations;
using KyrgyzTest.Application.Abstractions.OldDbAbstractions.TestResults;
using KyrgyzTest.Application.Abstractions.OldDbRegionAbstractions.Regulations;
using KyrgyzTest.Application.Abstractions.OldDbRegionAbstractions.TestResults;
using KyrgyzTest.Application.Contracts.Mailysearchs;
using KyrgyzTest.Application.Contracts.OldDbs.Regulations;
using KyrgyzTest.Application.Contracts.OldDbs.TestResults;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.KTOldDbs.TestResults.GetByStudentId;

public class GetStudentResultsQueryHandler(
        ITestResultRepository testResultRepository,
        ITestResultRegionRepository testResultRegionRepository,
        IRegulationRepository regulationRepository,
        IRegulationRegionRepository regulationRegionRepository
    )
    
    : IQueryHandler<GetStudentResultsQuery, Result<StudentResultResponse>>
{
    public async Task<Result<StudentResultResponse>> Handle(GetStudentResultsQuery request, CancellationToken cancellationToken)
    {
        var source = request.Args.Source;
        
        var studentId = request.Args.IdStudent;
        
        var results = new List<TestResultDto>();
        
        var regulations = new List<RegulationDto>();
        
        if(source == SearchSources.Region)
        {
            results = await testResultRegionRepository.GetByStudentIdAsync(studentId);
            
            regulations = await regulationRegionRepository.GetAll();
        } 
        else if (source == SearchSources.OldDb)
        {
            results = await testResultRepository.GetByStudentIdAsync(studentId);
            
            regulations  = await regulationRepository.GetAll();
        }
        
        if (results.Count == 0)
        {
            return new Error(KtOldDbErrorCode.ResultNotFound,
                $"Operation failed result id: {studentId} not found");
        }
          
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

        return new StudentResultResponse
        {
            TestResults = results
        };
    }
}

