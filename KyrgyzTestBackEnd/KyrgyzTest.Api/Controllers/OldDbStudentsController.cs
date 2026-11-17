using KyrgyzTest.Api.Extensions;
using KyrgyzTest.Application.Contracts.OldDbs.Students;
using KyrgyzTest.Application.Models.KTOldDbs.Students.Create;
using KyrgyzTest.Application.Models.KTOldDbs.Students.Delete;
using KyrgyzTest.Application.Models.KTOldDbs.Students.GetById;
using KyrgyzTest.Application.Models.KTOldDbs.Students.GetByPin;
using KyrgyzTest.Application.Models.KTOldDbs.Students.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KyrgyzTest.Api.Controllers;


[Route("api-archive-students")]
public class OldDbStudentsController(ISender sender) : ControllerBase
{
    [Authorize]
    [HttpGet("get-by-id")]
    public async Task<IActionResult> GetById([FromQuery] GetByStudentIdArgs query)
    {
        var response = await sender.Send(new GetByIdStudentQuery(query));

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => BadRequest(error.Message)
        );
    }
    
    [Authorize]
    [HttpGet("get-by-pin")]
    public async Task<IActionResult> GetById([FromQuery] GetStudentByPinArgs query)
    {
        var response = await sender.Send(new GetByPinStudentQuery(query));

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => BadRequest(error.Message)
        );
    }
    
    [Authorize]
    [HttpPut("change-student")]
    public async Task<IActionResult> Change([FromQuery] UpdateStudentArgs studentArgs)
    {
        var response = await sender.Send(new UpdateStudentCommand(studentArgs));

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => BadRequest(error.Message)
        );
    }
    
    [Authorize]
    [HttpPost("create-student")]
    public async Task<IActionResult> Create([FromQuery] CreateStudentArgs studentArgs)
    {
        var response = await sender.Send(new CreateStudentCommand(studentArgs));

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => BadRequest(error.Message)
        );
    }
    
    [Authorize]
    [HttpDelete("delete-student")]
    public async Task<IActionResult> Delete([FromQuery] DeleteStudentArgs deleteArgs)
    {
        var response = await sender.Send(new DeleteStudentCommand(deleteArgs));
        
        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => BadRequest(error.Message)
        );
    }
}