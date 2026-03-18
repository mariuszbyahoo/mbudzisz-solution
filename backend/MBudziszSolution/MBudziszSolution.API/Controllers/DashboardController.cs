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
            totalOrganizations = SeedData.Organizations.Count,
            totalUsers = SeedData.Users.Count,
            totalProjects = SeedData.Projects.Count,
            activeProjects = SeedData.Projects.Count(p => p.Status.Equals("active", StringComparison.OrdinalIgnoreCase)),
            totalTimeEntries = SeedData.TimeEntries.Count,
            totalInvoiced = SeedData.Invoices.Sum(i => i.Amount)
        });
    }
}
