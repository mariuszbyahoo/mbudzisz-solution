using MBudziszSolution.Data;
using Microsoft.AspNetCore.Mvc;

namespace MBudziszSolution.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll([FromQuery] string? orgId, [FromQuery] string? status)
    {
        var results = SeedData.Invoices.AsEnumerable();

        if (!string.IsNullOrEmpty(orgId))
            results = results.Where(i => i.OrgId == orgId);

        if (!string.IsNullOrEmpty(status))
            results = results.Where(i => i.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

        return Ok(results.ToList());
    }
}
