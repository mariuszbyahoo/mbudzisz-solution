using MBudziszSolution.Data;
using Microsoft.AspNetCore.Mvc;

namespace MBudziszSolution.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(
        [FromQuery] string? orgId,
        [FromQuery] string? role,
        [FromQuery] bool? active)
    {
        var results = SeedData.Users.AsEnumerable();

        if (!string.IsNullOrEmpty(orgId))
            results = results.Where(u => u.OrgId == orgId);

        if (!string.IsNullOrEmpty(role))
            results = results.Where(u => u.Role.Equals(role, StringComparison.OrdinalIgnoreCase));

        if (active.HasValue)
            results = results.Where(u => u.Active == active.Value);

        return Ok(results.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var user = SeedData.Users.FirstOrDefault(u => u.Id == id);

        if (user is null)
            return NotFound(new { error = "User not found" });

        return Ok(user);
    }
}
