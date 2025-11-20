using KyrgyzTest.Application.Abstractions;

namespace KyrgyzTest.Application.TextWorks.Reports.FrequencyExcelReport;

public record FrecuencyExcelReportQuery(string Text) : IQuery<byte[]>;