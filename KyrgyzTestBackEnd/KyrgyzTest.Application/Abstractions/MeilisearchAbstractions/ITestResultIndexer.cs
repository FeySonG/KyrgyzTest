namespace KyrgyzTest.Application.Abstractions.MeilisearchAbstractions;

public interface ITestResultIndexer
{
    Task RebuildIndexAsync();
}