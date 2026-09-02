using ClosedXML.Excel;
using KyrgyzTest.Application.Contracts.CertificateRecords;
using KyrgyzTest.Application.Extensions.Result;
using KyrgyzTest.Core.Models.CertificateRecords;

namespace KyrgyzTest.Application.Models.CertificateRecords;

public class ExcelReader
{
    private static readonly Dictionary<string, string[]> RequiredColumns = new()
    {
        ["Сертификаттын ээсинин аты-жөнү"] =
        [
            "Сертификаттын ээсинин аты-жөнү"
        ],

        ["Мекеменин аталышы"] =
        [
            "Мекеменин аталышы"
        ],

        ["Сертификаттын номуру"] =
        [
            "Сертификаттын номуру",
            "Сертификаттын номери"
        ],

        ["Деңгээли"] =
        [
            "Деңгээли",
            "Дэңгээли"
        ],

        ["Сертификаттын берилген күнү"] =
        [
            "Сертификаттын берилген күнү"
        ],

        ["Комментарии"] =
        [
            "Комментарии",
            "КомментариЙ"
        ]
    };

    public Result<List<CertificateRecord>> Read(Stream stream)
    {
        using var workbook = new XLWorkbook(stream);

        var worksheet = workbook.Worksheet(1);

        var headers = worksheet.Row(1)
            .CellsUsed()
            .ToDictionary(
                c => NormalizeHeader(c.GetString()),
                c => c.Address.ColumnNumber,
                StringComparer.OrdinalIgnoreCase);

        var missingColumns = RequiredColumns
            .Where(column => !column.Value
                .Any(alias => headers.ContainsKey(NormalizeHeader(alias))))
            .Select(column => column.Key)
            .ToList();

        if (missingColumns.Count > 0)
        {
            return Result.Fail<List<CertificateRecord>>(
                "CertificateRecord.MissingColumns",
                $"В Excel отсутствуют обязательные колонки: {string.Join(", ", missingColumns)}. " +
                $"Найдены колонки: {string.Join(", ", headers.Keys)}.");
        }

        var records = new List<CertificateRecord>();

        foreach (var row in worksheet.RowsUsed().Skip(1))
        {
            if (row.IsEmpty())
                continue;

            var received = GetString(row, headers, "Сертификаттын ээсинин аты-жөнү");
            var organization = GetString(row, headers, "Мекеменин аталышы");
            var certificateNumber = GetString(row, headers, "Сертификаттын номуру");
            var level = GetString(row, headers, "Деңгээли");
            var issueDate = GetDate(row, headers, "Сертификаттын берилген күнү");
            var additionalInfo = GetString(row, headers, "Комментарии");

            if (string.IsNullOrWhiteSpace(certificateNumber))
            {
                return Result.Fail<List<CertificateRecord>>(
                    "CertificateRecord.EmptyCertificateNumber",
                    $"Строка {row.RowNumber()}. Не заполнен номер сертификата.");
            }

            if (!certificateNumber.All(char.IsDigit))
            {
                return Result.Fail<List<CertificateRecord>>(
                    "CertificateRecord.InvalidCertificateNumber",
                    $"Строка {row.RowNumber()}. Номер сертификата должен содержать только цифры.");
            }

            records.Add(new CertificateRecord
            {
                Received = received,
                Organization = organization,
                CertificateNumber = certificateNumber,
                Level = level,
                IssueDate = issueDate,
                AdditionalInfo = additionalInfo
            });
        }

        return Result.Ok(records);
    }

    

    private static string GetString(
        IXLRow row,
        Dictionary<string, int> headers,
        string columnName)
    {
        return row.Cell(GetColumnNumber(headers, columnName)).GetString().Trim();
    }

    private static long GetLong(
        IXLRow row,
        Dictionary<string, int> headers,
        string columnName)
    {
        return row.Cell(GetColumnNumber(headers, columnName)).GetValue<long>();
    }

    private static DateTime GetDate(
        IXLRow row,
        Dictionary<string, int> headers,
        string columnName)
    {
        return row.Cell(GetColumnNumber(headers, columnName)).GetDateTime();
    }

    private static int GetColumnNumber(
        IReadOnlyDictionary<string, int> headers,
        string columnName)
    {
        var aliases = RequiredColumns[columnName];
        var matchedHeader = aliases
            .Select(NormalizeHeader)
            .First(headers.ContainsKey);

        return headers[matchedHeader];
    }

    private static string NormalizeHeader(string value) =>
        string.Join(
            " ",
            value.Replace('\uFEFF', ' ')
                .Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries));
}
