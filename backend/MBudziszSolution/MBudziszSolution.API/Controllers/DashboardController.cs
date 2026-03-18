using MBudziszSolution.Data;
using Microsoft.AspNetCore.Mvc;

namespace MBudziszSolution.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            organizationCount = SeedData.Organizations.Count,
            userCount = SeedData.Users.Count,
            projectCount = SeedData.Projects.Count,
            timeEntryCount = SeedData.TimeEntries.Count,
            invoiceCount = SeedData.Invoices.Count,
            totalHoursLogged = SeedData.TimeEntries.Sum(te => te.Hours),
            totalInvoiced = SeedData.Invoices.Sum(i => i.Amount),
            activeProjectCount = SeedData.Projects.Count(p => p.Status == "active"),
            activeUserCount = SeedData.Users.Count(u => u.Active)
        });
    }
}
