using MBudziszSolution.Services;
using Microsoft.AspNetCore.Mvc;

namespace MBudziszSolution.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly AggregationService _aggregation;

    public DashboardController(AggregationService aggregation)
    {
        _aggregation = aggregation;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_aggregation.GetDashboardStats());
    }
}
