using MBudziszSolution.Data;
using MBudziszSolution.Services;
using Microsoft.AspNetCore.Mvc;

namespace MBudziszSolution.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly AggregationService _aggregation;

    public ProjectsController(AggregationService aggregation)
    {
        _aggregation = aggregation;
    }

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
        var totalHoursLogged = _aggregation.GetTotalHoursLogged(id);

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
