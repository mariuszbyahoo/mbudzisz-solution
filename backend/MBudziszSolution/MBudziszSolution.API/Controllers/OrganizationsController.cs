using MBudziszSolution.Data;
using Microsoft.AspNetCore.Mvc;

namespace MBudziszSolution.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll([FromQuery] string? tier, [FromQuery] string? industry)
    {
        var results = SeedData.Organizations.AsEnumerable();

        if (!string.IsNullOrEmpty(tier))
            results = results.Where(o => o.Tier.Equals(tier, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(industry))
            results = results.Where(o => o.Industry.Equals(industry, StringComparison.OrdinalIgnoreCase));

        return Ok(results.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var org = SeedData.Organizations.FirstOrDefault(o => o.Id == id);

        if (org is null)
            return NotFound(new { error = "Organization not found" });

        return Ok(org);
    }

    [HttpGet("{id}/summary")]
    public IActionResult GetSummary(string id)
    {
        var org = SeedData.Organizations.FirstOrDefault(o => o.Id == id);

        if (org is null)
            return NotFound(new { error = "Organization not found" });

        var userCount = SeedData.Users.Count(u => u.OrgId == id);
        var projectCount = SeedData.Projects.Count(p => p.OrgId == id);
        var totalInvoiced = SeedData.Invoices
            .Where(i => i.OrgId == id)
            .Sum(i => i.Amount);

        return Ok(new
        {
            organization = org,
            projectCount,
            userCount,
            totalInvoiced,
            currency = org.Settings.Currency
        });
    }
}
