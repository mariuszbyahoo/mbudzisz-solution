using MBudziszSolution.Data;
using Microsoft.AspNetCore.Mvc;

namespace MBudziszSolution.Controllers;

[ApiController]
[Route("api/time-entries")]
public class TimeEntriesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] string? userId,
        [FromQuery] string? projectId,
        [FromQuery] DateOnly? from,
        [FromQuery] DateOnly? to)
    {
        var results = SeedData.TimeEntries.AsEnumerable();

        if (!string.IsNullOrEmpty(userId))
            results = results.Where(te => te.UserId == userId);

        if (!string.IsNullOrEmpty(projectId))
            results = results.Where(te => te.ProjectId == projectId);

        if (from.HasValue)
            results = results.Where(te => DateOnly.Parse(te.Date) >= from.Value);

        if (to.HasValue)
            results = results.Where(te => DateOnly.Parse(te.Date) <= to.Value);

        return Ok(results.ToList());
    }
}
