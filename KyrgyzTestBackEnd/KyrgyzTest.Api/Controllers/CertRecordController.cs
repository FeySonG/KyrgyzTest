using KyrgyzTest.Api.Extensions;
using KyrgyzTest.Application.Models.CertificateRecords.AddFromExcelFile;
using KyrgyzTest.Application.Models.CertificateRecords.Delete;
using KyrgyzTest.Application.Models.CertificateRecords.GetByDateRange;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KyrgyzTest.Api.Controllers;

[ApiController]
[Route("api-cert-record")]
public class CertRecordController(ISender sender) : ControllerBase
{
    
    [HttpPost("add-from-excel")]
    public async Task<IActionResult> AddFromExcel([FromForm]IFormFile? file)
    {
        if (file is null || file.Length == 0)
            return BadRequest("Файл не передан");
        
        var response = await sender.Send(new AddFromExcelCommand(file.OpenReadStream(), file.FileName));
        
        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => BadRequest(error.Message)
        );
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var response = await sender.Send(new DeleteCertificateRecordCommand(id));

        return response.Match(
            onSuccess: _ => NoContent(),
            onFailure: error => NotFound(error.Message)
        );
    }

    [HttpGet("by-issue-date-range")]
    public async Task<IActionResult> GetByIssueDateRange(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        var response = await sender.Send(
            new GetCertificateRecordsByDateRangeQuery(startDate, endDate));

        return response.Match(
            onSuccess: value => Ok(value),
            onFailure: error => BadRequest(error.Message)
        );
    }
}
