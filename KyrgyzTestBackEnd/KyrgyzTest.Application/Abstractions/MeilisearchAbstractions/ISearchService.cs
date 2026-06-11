using KyrgyzTest.Application.Contracts.Mailysearchs;
using KyrgyzTest.Application.Contracts.OldDbs.TestResults;

namespace KyrgyzTest.Application.Abstractions.MeilisearchAbstractions;

public interface ISearchService 
{
    Task<List<TestResultSearchDto>> SearchAsync(string query);
    
}