using KyrgyzTest.Application.Abstractions.MeilisearchAbstractions;
using KyrgyzTest.Application.Contracts.CertificateRecords;
using Meilisearch;
using Microsoft.Extensions.Configuration;
using MeilisearchIndex = Meilisearch.Index;

namespace KyrgyzTest.DAL.Services.Meilisearchs;

/// <summary>
/// Синхронизирует DTO записей сертификатов с Meilisearch.
/// Поисковый индекс намеренно содержит DTO, а не EF-сущности.
/// </summary>
public sealed class CertificateRecordIndexer : ICertificateRecordIndexer
{
    private const string PrimaryKey = "id";
    private const double TaskTimeoutMs = 60_000;
    private const int TaskPollingIntervalMs = 100;

    private readonly MeilisearchClient _client;
    private readonly SemaphoreSlim _configurationLock = new(1, 1);
    private bool _isConfigured;

    public CertificateRecordIndexer(IConfiguration configuration)
    {
        var url = configuration["Meilisearch:Url"]
                  ?? throw new InvalidOperationException("Meilisearch:Url is not configured.");
        var apiKey = configuration["Meilisearch:ApiKey"];

        _client = string.IsNullOrWhiteSpace(apiKey)
            ? new MeilisearchClient(url)
            : new MeilisearchClient(url, apiKey);
    }

    /// <summary>Добавляет новые DTO или заменяет документы с совпадающими идентификаторами.</summary>
    public async Task AddOrUpdateAsync(
        IEnumerable<CertificateRecordDto> records,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(records);

        var documents = records.ToList();
        if (documents.Count == 0)
            return;

        var index = await GetConfiguredIndexAsync(cancellationToken);
        var task = await index.AddDocumentsAsync(documents, PrimaryKey, cancellationToken);

        // API только ставит операцию в очередь, поэтому ждём доступности документов для поиска.
        await WaitForTaskAsync(task, index, cancellationToken);
    }

    /// <summary>Удаляет одну запись из индекса по идентификатору из базы данных.</summary>
    public async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var index = await GetConfiguredIndexAsync(cancellationToken);
        var task = await index.DeleteOneDocumentAsync(id.ToString(), cancellationToken);

        await WaitForTaskAsync(task, index, cancellationToken);
    }

    /// <summary>Полностью заменяет документы в индексе записей сертификатов.</summary>
    public async Task RebuildAsync(
        IEnumerable<CertificateRecordDto> records,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(records);

        var index = await GetConfiguredIndexAsync(cancellationToken);
        var deleteTask = await index.DeleteAllDocumentsAsync(cancellationToken);

        // Не добавляем документы, пока Meilisearch полностью не завершит удаление старых.
        await WaitForTaskAsync(deleteTask, index, cancellationToken);
        await AddOrUpdateAsync(records, cancellationToken);
    }

    private async Task<MeilisearchIndex> GetConfiguredIndexAsync(CancellationToken cancellationToken)
    {
        var index = _client.Index(MeiliIndexes.CertificateRecords);

        if (_isConfigured)
            return index;

        await _configurationLock.WaitAsync(cancellationToken);
        try
        {
            if (_isConfigured)
                return index;

            // Полнотекстовый поиск ограничен полями, полезными для оператора.
            await WaitForTaskAsync(await index.UpdateSearchableAttributesAsync(new[]
            {
                "certificateNumber", "received", "organization", "level", "additionalInfo"
            }, cancellationToken), index, cancellationToken);

            // По этим полям можно фильтровать результаты Meilisearch.
            await WaitForTaskAsync(await index.UpdateFilterableAttributesAsync(new[]
            {
                "organization", "level", "issueDate"
            }, cancellationToken), index, cancellationToken);

            // Результаты поиска можно сортировать по дате.
            await WaitForTaskAsync(await index.UpdateSortableAttributesAsync(new[]
            {
                "issueDate"
            }, cancellationToken), index, cancellationToken);

            _isConfigured = true;
            return index;
        }
        finally
        {
            _configurationLock.Release();
        }
    }

    private static Task WaitForTaskAsync(
        TaskInfo task,
        MeilisearchIndex index,
        CancellationToken cancellationToken) =>
        index.WaitForTaskAsync(
            task.TaskUid,
            TaskTimeoutMs,
            TaskPollingIntervalMs,
            cancellationToken);
}
