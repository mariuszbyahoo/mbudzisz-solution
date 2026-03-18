using MBudziszSolution.Models;

namespace MBudziszSolution.Services;

public record DashboardStats
{
    public int TotalOrganizations { get; init; }
    public int TotalUsers { get; init; }
    public int TotalProjects { get; init; }
    public int ActiveProjects { get; init; }
    public int TotalTimeEntries { get; init; }
    public decimal TotalInvoiced { get; init; }
}

public record OrganizationSummaryStats
{
    public int UserCount { get; init; }
    public int ProjectCount { get; init; }
    public decimal TotalInvoiced { get; init; }
}

public class AggregationService
{
    private readonly IReadOnlyList<Organization> _organizations;
    private readonly IReadOnlyList<User> _users;
    private readonly IReadOnlyList<Project> _projects;
    private readonly IReadOnlyList<TimeEntry> _timeEntries;
    private readonly IReadOnlyList<Invoice> _invoices;

    public AggregationService(
        IReadOnlyList<Organization> organizations,
        IReadOnlyList<User> users,
        IReadOnlyList<Project> projects,
        IReadOnlyList<TimeEntry> timeEntries,
        IReadOnlyList<Invoice> invoices)
    {
        _organizations = organizations;
        _users = users;
        _projects = projects;
        _timeEntries = timeEntries;
        _invoices = invoices;
    }

    public DashboardStats GetDashboardStats()
    {
        return new DashboardStats
        {
            TotalOrganizations = _organizations.Count,
            TotalUsers = _users.Count,
            TotalProjects = _projects.Count,
            ActiveProjects = _projects.Count(p => p.Status.Equals("active", StringComparison.OrdinalIgnoreCase)),
            TotalTimeEntries = _timeEntries.Count,
            TotalInvoiced = _invoices.Sum(i => i.Amount)
        };
    }

    public OrganizationSummaryStats GetOrganizationSummary(string orgId)
    {
        return new OrganizationSummaryStats
        {
            UserCount = _users.Count(u => u.OrgId == orgId),
            ProjectCount = _projects.Count(p => p.OrgId == orgId),
            TotalInvoiced = _invoices.Where(i => i.OrgId == orgId).Sum(i => i.Amount)
        };
    }

    public int GetTotalHoursLogged(string projectId)
    {
        return _timeEntries
            .Where(te => te.ProjectId == projectId)
            .Sum(te => te.Hours);
    }
}
