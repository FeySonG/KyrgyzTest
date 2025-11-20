using ClosedXML.Excel;
using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.TextWorks.TextManipulation;

namespace KyrgyzTest.Application.TextWorks.Reports.FrequencyExcelReport;

public class FrequencyExcelReportQueryHandler : IQueryHandler<FrecuencyExcelReportQuery, byte[]>
{
    public Task<byte[]> Handle(FrecuencyExcelReportQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
            throw new ArgumentNullException(nameof(request.Text), "Текст не может быть пустым");

        // 1️⃣ Подсчёт частоты слов
        var analyze = FrecuencyAnalyze.Analyze(request.Text);

        using var workbook = new XLWorkbook();
        var ws = workbook.AddWorksheet("Frequencies");

        ws.Cell(1, 1).Value = "Слово";
        ws.Cell(1, 2).Value = "Количество";

        int row = 2;
        foreach (var kv in analyze.Frequency.OrderByDescending(k => k.Value))
        {
            ws.Cell(row, 1).Value = kv.Key;
            ws.Cell(row, 2).Value = kv.Value;
            row++;
        }

        ws.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);

        return Task.FromResult(stream.ToArray());
    }
}