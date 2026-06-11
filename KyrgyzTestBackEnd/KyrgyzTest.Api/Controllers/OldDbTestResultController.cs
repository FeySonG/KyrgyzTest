using KyrgyzTest.Application.Contracts.OldDbs.Students;
using KyrgyzTest.Application.Contracts.OldDbs.TestResults;
using KyrgyzTest.Application.Models.KTOldDbs.Regulations.GetByIdRegulation;
using KyrgyzTest.Application.Models.KTOldDbs.TestResults.GetById;
using KyrgyzTest.Application.Models.KTOldDbs.TestResults.GetByStudentId;
using KyrgyzTest.Application.Models.KTOldDbs.TestResults.SearchResultByName;
using KyrgyzTest.DAL.Services.Meilisearchs;
using KyrgyzTest.OldDb;
using KyrgyzTest.OldDbRegion;
using MediatR;
using Meilisearch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SibersCRM.API.Extensions;

namespace KyrgyzTest.Api.Controllers;

[Route("api-archive")]
public class OldDbTestResultController(ISender sender,
                                LegacyDbContext dbContext,
                                LegacyDbRegionContext dbRegionContext) : ControllerBase
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
    
    [Authorize]
    [HttpGet("get-by-student-id")]
    public async Task<IActionResult> GetByStudentId(GetByStudentIdArgs args)
    {
        var response = await sender.Send(new GetStudentResultsQuery(args));

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => BadRequest(error.Message)
        );
    }

    [Authorize]
    [HttpGet("get-regulation")]
    public async Task<IActionResult> GetRegulationById(int id)
    {
        var response = await sender.Send(new GetByIdRegulationQuery(id));

        return response.Match(
            onSuccess: value => Ok(response.Value),
            onFailure: error => BadRequest(error.Message)
        );
    }

    [Authorize]
    [HttpGet("search-by-name")]
    public async Task<IActionResult> SearchByName([FromQuery] ResultSearchArgs query)
    {
        var response = await sender.Send(new SearchResultByNameQuery(query));

        return response.Match(
            onSuccess: value => Ok(value),
            onFailure: error => BadRequest(error.Message)
        ); 
    }
    
    
    [HttpPost("rebuild-search")]
    public async Task<IActionResult> RebuildSearch(
        [FromServices] TestResultIndexer indexer)
    {
        await indexer.RebuildAsync();

        return Ok();
    }
}