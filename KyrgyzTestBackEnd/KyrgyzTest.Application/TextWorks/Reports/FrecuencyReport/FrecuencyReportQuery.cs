using KyrgyzTest.Application.Abstractions;

namespace KyrgyzTest.Application.TextWorks.Reports.FrecuencyReport;

public record FrecuencyReportQuery(string Text) : IQuery<byte[]>;