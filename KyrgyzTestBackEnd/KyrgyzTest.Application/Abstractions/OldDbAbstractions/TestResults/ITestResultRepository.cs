using KyrgyzTest.Application.Contracts.OldDbs.TestResults;

namespace KyrgyzTest.Application.Abstractions.OldDbAbstractions.TestResults;

public interface ITestResultRepository
{
    Task<TestResultDto?> GetByIdAsync(int id);

    Task<List<TestResultDto>> GetByStudentIdAsync(int studentId);

    Task<List<TestResultDto>> SearchCandidatesAsync(string query);
    
    Task<List<TestResultDto>> GetAllAsync();
}