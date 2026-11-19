using KyrgyzTest.Application.Contracts.OldDbs.TestResults;

namespace KyrgyzTest.Application.Abstractions.OldDbRegionAbstractions.TestResults;

public interface ITestResultRegionRepository
{
    Task<List<TestResultDto>> GetAllAsync();
    Task<TestResultDto?> GetByIdAsync(int id);

    Task<List<TestResultDto>> GetByStudentIdAsync(int studentId);

    Task<List<TestResultDto>> SearchCandidatesAsync(string query);
}