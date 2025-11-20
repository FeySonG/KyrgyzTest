using KyrgyzTest.Application.Contracts.TextWorks;
using KyrgyzTest.Application.Contracts.TextWorks.TextAnalysis;
using KyrgyzTest.Application.TextWorks.Reports.FrecuencyReport;
using KyrgyzTest.Application.TextWorks.Reports.FrequencyExcelReport;
using KyrgyzTest.Application.TextWorks.TextAnalysis;
using KyrgyzTest.Application.TextWorks.TextSearch;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KyrgyzTest.Api.Controllers;

[ApiController]
[Route("api/text-works")]
public class TextWorkController(ISender sender) : ControllerBase
{
    [HttpPost("text-analyze")]
    public async Task<IActionResult> GetAnalysis([FromBody] TextRequest request)
    {
        try
        {
            var response = await sender.Send(new TextAnalysisQuery(request.Text));
            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPost("text-analyze-report")]
    public async Task<IActionResult> GetAnalysisReport([FromBody] TextRequest request)
    {
        try
        {
            var response = await sender.Send(new FrecuencyReportQuery(request.Text));

            return File(response, "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "Частотный Анализ.docx");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPost("text-analyze-excel-report")]
    public async Task<IActionResult> GetAnalysisExcelReport([FromBody] TextRequest request)
    {
        try
        {
            var response = await sender.Send(new FrecuencyExcelReportQuery(request.Text));

            return File(response, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "frequency-report.xlsx");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPost("text-search")]
    public async Task<IActionResult> Search([FromBody] SearchRequest query)
    {
        try
        {
            var response = await sender.Send(new TextSearchQuery(query));

            return Ok(response);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}