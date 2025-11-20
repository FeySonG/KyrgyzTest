using KyrgyzTest.Application.Abstractions;
using KyrgyzTest.Application.TextWorks.TextManipulation;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace KyrgyzTest.Application.TextWorks.Reports.FrecuencyReport;

public class FrecuencyReportQueryHandler : IQueryHandler<FrecuencyReportQuery, byte[]>
{
    public Task<byte[]> Handle(FrecuencyReportQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
            throw new ArgumentNullException(nameof(request.Text), "Текст не может быть пустым");

        // 1️⃣ Подсчёт частоты слов
        var analyze = FrecuencyAnalyze.Analyze(request.Text);

        // 2️⃣ Создаём временный файл отчёта
        string filePath = Path.Combine(Path.GetTempPath(), $"FrequencyReport_{Guid.NewGuid()}.docx");

        using (var doc = DocX.Create(filePath))
        {
            doc.InsertParagraph("ЧАСТОТНЫЙ АНАЛИЗ ТЕКСТА")
                .FontSize(16).Bold().Alignment = Alignment.center;

            doc.InsertParagraph($"Дата отчёта: {DateTime.Now}\n");
            doc.InsertParagraph($"Всего слов: {analyze.WordCount}");
            doc.InsertParagraph($"Уникальных слов: {analyze.Frequency.Count}");
            doc.InsertParagraph($"Количество символов: {analyze.CharCount}");
            doc.InsertParagraph("\nТОП-30 слов:\n").Bold();

            // 3️⃣ Сортируем слова по частоте
            var topWords = analyze.Frequency
                .OrderByDescending(kv => kv.Value)
                .Take(30)
                .ToList();

            // 4️⃣ Таблица: первая строка — заголовок + до 20 строк данных
            var table = doc.AddTable(topWords.Count + 1, 2);
            table.Design = TableDesign.LightList;

            table.Rows[0].Cells[0].Paragraphs[0].Append("Слово").Bold();
            table.Rows[0].Cells[1].Paragraphs[0].Append("Частота").Bold();

            for (int i = 0; i < topWords.Count; i++)
            {
                table.Rows[i + 1].Cells[0].Paragraphs[0].Append(topWords[i].Key);
                table.Rows[i + 1].Cells[1].Paragraphs[0].Append(topWords[i].Value.ToString());
            }

            doc.InsertTable(table);
            doc.Save();
        }

        // 5️⃣ Возвращаем файл клиенту
        byte[] fileBytes = File.ReadAllBytes(filePath);
        File.Delete(filePath);

        return Task.FromResult(fileBytes);
    }
}