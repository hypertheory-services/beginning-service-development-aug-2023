using IssueTrackerApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace IssueTrackerApi.Controllers;

public class HomeController : ControllerBase
{
    [HttpGet("/")]
    public async Task<ActionResult> GetHomeInfo()
    {
        var response = new InfoModelResponse
        {
            CheckedAt = DateTime.Now,
            Description = "Fake Version - Issues API",
            OwnedBy = "Company.com",

        };
        response.Links.Add("/issues", new InfoLink { Description = "Get info about issues, etc." });
        response.Links.Add("/", new InfoLink { Description = "This Document" });
        response.Support.Email = "bob@aol.com";
        response.Support.Phone = "1234567890";
        return Ok(response);
    }
}
