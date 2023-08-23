using IssueTrackerApi.Models;
using IssueTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace IssueTrackerApi.Controllers;

public class HomeController : ControllerBase
{

    private readonly BusinessClockApiAdapter _businessClockApiAdapter;

    public HomeController(BusinessClockApiAdapter businessClockApiAdapter)
    {
        _businessClockApiAdapter = businessClockApiAdapter;
    }

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

        var contactInfo = await _businessClockApiAdapter.GetStatusAsync();

        response.Support.Email = contactInfo.SupportContact.Email;
        response.Support.Phone = contactInfo.SupportContact.Phone;
        return Ok(response);
    }
}
