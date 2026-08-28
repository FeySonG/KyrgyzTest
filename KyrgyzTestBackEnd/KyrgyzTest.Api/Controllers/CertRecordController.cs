using KyrgyzTest.Api.Extensions;
using KyrgyzTest.Application.Contracts.CertificateRecords;
using KyrgyzTest.Application.Models.CertificateRecords.AddFromExcelFile;
using KyrgyzTest.Application.Models.CertificateRecords.Delete;
using KyrgyzTest.Application.Models.CertificateRecords.GetAll;
using KyrgyzTest.Application.Models.CertificateRecords.GetByDateRange;
using KyrgyzTest.Application.Models.CertificateRecords.GetById;
using KyrgyzTest.Application.Models.CertificateRecords.SearchByName;
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

    [HttpGet("get-by-id")]
    public async Task<IActionResult> GetById(long id)
    {
        var response = await sender.Send(new GetByIdCertRecordQuery(id));

        return response.Match(
            onSuccess: record => Ok(record), 
            onFailure: error => BadRequest(error.Message));
    }

    [HttpGet("search-by-received")]
    public async Task<IActionResult> SearchByReceived([FromQuery] SearchByReceivedQuery query)
    {
        var response = await sender.Send(query);
        return response.Match(
            onSuccess: record => Ok(record),
            onFailure: error => BadRequest(error.Message)
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

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        var response = await sender.Send(
            new GetAllCertRecordQuery());

        return response.Match(
            onSuccess: value => Ok(value),
            onFailure: error => BadRequest(error.Message)
        );
    }

    [HttpGet("search-by-certnumber")]
    public async Task<IActionResult> SearchByCertnumber([FromQuery] SearchByReceivedQuery query)
    {
        var response = await sender.Send(query);
        return response.Match(
            onSuccess: value => Ok(value),
            onFailure: error => BadRequest(error.Message));
    }
}
