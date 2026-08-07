using ClosedXML.Excel;
using KyrgyzTest.Application.Contracts.CertificateRecords;
using KyrgyzTest.Application.Extensions.Result;
using KyrgyzTest.Core.Models.CertificateRecords;

namespace KyrgyzTest.Application.Models.CertificateRecords;

public class ExcelReader
{
    public Result<List<CertificateRecord>> Read(Stream stream)
    {
        using var workbook = new XLWorkbook(stream);

        var worksheet = workbook.Worksheet(1);

        var headers = worksheet.Row(1)
            .CellsUsed()
            .ToDictionary(
                c => c.GetString().Trim(),
                c => c.Address.ColumnNumber);

        var records = new List<CertificateRecord>();

        foreach (var row in worksheet.RowsUsed().Skip(1))
        {
            if (row.IsEmpty())
                continue;

            var received = GetString(row, headers, "Сертификаттын ээсинин аты-жөнү");
            var organization = GetString(row, headers, "Мекеменин аталышы");
            var certificateNumber = GetString(row, headers, "Сертификаттын номери");
            var level = GetString(row, headers, "Деңгээли");
            var issueDate = GetDate(row, headers, "Сертификаттын берилген күнү");
            var additionalInfo = GetString(row, headers, "Эскертүү");

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
        return row.Cell(headers[columnName]).GetString().Trim();
    }

    private static long GetLong(
        IXLRow row,
        Dictionary<string, int> headers,
        string columnName)
    {
        return row.Cell(headers[columnName]).GetValue<long>();
    }

    private static DateTime GetDate(
        IXLRow row,
        Dictionary<string, int> headers,
        string columnName)
    {
        return row.Cell(headers[columnName]).GetDateTime();
    }
}