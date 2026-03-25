using KyrgyzTest.Application.Models.KTOldDbs.TestResults.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SibersCRM.API.Extensions;

namespace KyrgyzTest.Api.Controllers;

[Route("api-old-result")]
public class OldDbController(ISender sender) : ControllerBase
{
    [Authorize]
    [HttpGet("get-by-{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await sender.Send(new TestResultByIdQuery(id));

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => BadRequest(error.Message)
        );
    }
}