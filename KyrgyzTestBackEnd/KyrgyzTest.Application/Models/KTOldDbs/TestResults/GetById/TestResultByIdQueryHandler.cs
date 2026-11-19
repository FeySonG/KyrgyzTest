using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.Abstractions.OldDbAbstractions.TestResults;
using KyrgyzTest.Application.Contracts.OldDbs.TestResults;
using KyrgyzTest.Application.Extensions.Result;

namespace KyrgyzTest.Application.Models.KTOldDbs.TestResults.GetById;

public class TestResultByIdQueryHandler(
                            ITestResultRepository testResultRepository
                            ) : IQueryHandler<TestResultByIdQuery, Result<TestResultDto>>
{
    public async Task<Result<TestResultDto>> Handle(TestResultByIdQuery request, CancellationToken cancellationToken)
    {
        var testResult = await testResultRepository.GetByIdAsync(request.Id);
        if (testResult is null) 
        {
             return new Error(KtOldDbErrorCode.ResultNotFound, $"Operation failed result id: {request.Id} not found");
        }
        
        return testResult;
    }
    
    
}