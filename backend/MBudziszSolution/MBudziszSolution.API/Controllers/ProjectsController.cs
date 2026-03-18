using MBudziszSolution.Data;
using Microsoft.AspNetCore.Mvc;

namespace MBudziszSolution.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll([FromQuery] string? orgId, [FromQuery] string? status)
    {
        var results = SeedData.Projects.AsEnumerable();

        if (!string.IsNullOrEmpty(orgId))
            results = results.Where(p => p.OrgId == orgId);

        if (!string.IsNullOrEmpty(status))
            results = results.Where(p => p.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

        return Ok(results.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var project = SeedData.Projects.FirstOrDefault(p => p.Id == id);

        if (project is null)
            return NotFound(new { error = "Project not found" });

        var organization = SeedData.Organizations.FirstOrDefault(o => o.Id == project.OrgId);
        var totalHoursLogged = SeedData.TimeEntries
            .Where(te => te.ProjectId == id)
            .Sum(te => te.Hours);

        return Ok(new
        {
            project.Id,
            project.OrgId,
            project.Name,
            project.Status,
            project.BudgetHours,
            project.StartDate,
            project.EndDate,
            project.Description,
            organization,
            totalHoursLogged
        });
    }
}
