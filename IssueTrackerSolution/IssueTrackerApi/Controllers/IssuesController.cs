using IssueTrackerApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace IssueTrackerApi.Controllers;

[ApiController] // validate all [FromBody] parameters, and if they fail, return a 400 with an application/problem.json
public class IssuesController : ControllerBase
{

    // GET /greeting
    [HttpGet("/greeting")]
    public async Task<ActionResult> GetTheGreeting()
    {
        return Ok("Nice to see you");
    }

    [HttpPost("/issues")]
    public async Task<ActionResult> AddIssue([FromBody] IssueCreateRequest request)
    {
  

        var issue = new IssueResponse
        {
            Id = Guid.NewGuid(),
            Application = request.Application,
            ContactEmail = request.ContactEmail,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            Status = IssueStatus.Open
        };

        // Save it to a database

        return Ok(issue);
    }
}
