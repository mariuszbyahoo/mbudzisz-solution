using MBudziszSolution.Models;
using MBudziszSolution.Services;

namespace MBudziszSolution.Tests.Unit.Services;

public class AggregationServiceTests
{
    // ── Helpers: minimal model factories ─────────────────────────────

    private static Organization MakeOrg(string id, string tier = "enterprise") => new()
    {
        Id = id, Name = "Org", Slug = "org", Industry = "Tech", Tier = tier,
        ContactEmail = "a@b.com", CreatedAt = "2024-01-01", Description = "-",
        Settings = new OrganizationSettings { Timezone = "UTC", Currency = "USD", AllowOvertime = false, DefaultLocale = "en" },
        Metadata = new OrganizationMetadata { Source = "test" }
    };

    private static User MakeUser(string id, string orgId) => new()
    {
        Id = id, OrgId = orgId, Email = "a@b.com", Name = "User", Role = "member", Active = true, Bio = "-"
    };

    private static Project MakeProject(string id, string orgId, string status = "active") => new()
    {
        Id = id, OrgId = orgId, Name = "Project", Status = status, BudgetHours = 100, Description = "-"
    };

    private static TimeEntry MakeTimeEntry(string id, string projectId, int hours) => new()
    {
        Id = id, UserId = "u1", ProjectId = projectId, Date = "2024-03-01", Hours = hours, Description = "-"
    };

    private static Invoice MakeInvoice(string id, string orgId, decimal amount) => new()
    {
        Id = id, OrgId = orgId, ProjectId = "p1", Amount = amount, Currency = "USD", Status = "sent", Description = "-"
    };

    private static AggregationService CreateService(
        IReadOnlyList<Organization>? orgs = null,
        IReadOnlyList<User>? users = null,
        IReadOnlyList<Project>? projects = null,
        IReadOnlyList<TimeEntry>? timeEntries = null,
        IReadOnlyList<Invoice>? invoices = null)
    {
        return new AggregationService(
            orgs ?? [],
            users ?? [],
            projects ?? [],
            timeEntries ?? [],
            invoices ?? []);
    }

    // ── Dashboard Stats ─────────────────────────────────────────────

    [Fact]
    public void GetDashboardStats_ReturnsCorrectEntityCounts()
    {
        var svc = CreateService(
            orgs: [MakeOrg("o1"), MakeOrg("o2")],
            users: [MakeUser("u1", "o1"), MakeUser("u2", "o1"), MakeUser("u3", "o2")],
            projects: [MakeProject("p1", "o1"), MakeProject("p2", "o2", "completed")],
            timeEntries: [MakeTimeEntry("te1", "p1", 4), MakeTimeEntry("te2", "p1", 6)],
            invoices: [MakeInvoice("i1", "o1", 1000), MakeInvoice("i2", "o2", 2000)]
        );

        var stats = svc.GetDashboardStats();

        Assert.Equal(2, stats.TotalOrganizations);
        Assert.Equal(3, stats.TotalUsers);
        Assert.Equal(2, stats.TotalProjects);
        Assert.Equal(1, stats.ActiveProjects);
        Assert.Equal(2, stats.TotalTimeEntries);
        Assert.Equal(3000m, stats.TotalInvoiced);
    }

    [Fact]
    public void GetDashboardStats_CountsActiveProjects_CaseInsensitive()
    {
        var svc = CreateService(projects:
        [
            MakeProject("p1", "o1", "active"),
            MakeProject("p2", "o1", "Active"),
            MakeProject("p3", "o1", "ACTIVE"),
            MakeProject("p4", "o1", "completed")
        ]);

        Assert.Equal(3, svc.GetDashboardStats().ActiveProjects);
    }

    [Fact]
    public void GetDashboardStats_WithEmptyCollections_ReturnsZeros()
    {
        var stats = CreateService().GetDashboardStats();

        Assert.Equal(0, stats.TotalOrganizations);
        Assert.Equal(0, stats.TotalUsers);
        Assert.Equal(0, stats.TotalProjects);
        Assert.Equal(0, stats.ActiveProjects);
        Assert.Equal(0, stats.TotalTimeEntries);
        Assert.Equal(0m, stats.TotalInvoiced);
    }

    [Fact]
    public void GetDashboardStats_SumsInvoiceAmountsWithDecimalPrecision()
    {
        var svc = CreateService(invoices:
        [
            MakeInvoice("i1", "o1", 10000.50m),
            MakeInvoice("i2", "o2", 25000.75m)
        ]);

        Assert.Equal(35001.25m, svc.GetDashboardStats().TotalInvoiced);
    }

    // ── Organization Summary ────────────────────────────────────────

    [Fact]
    public void GetOrganizationSummary_ReturnsCorrectAggregatesForOrg()
    {
        var svc = CreateService(
            users: [MakeUser("u1", "o1"), MakeUser("u2", "o1"), MakeUser("u3", "o2")],
            projects: [MakeProject("p1", "o1"), MakeProject("p2", "o1"), MakeProject("p3", "o2")],
            invoices: [MakeInvoice("i1", "o1", 5000), MakeInvoice("i2", "o1", 3000), MakeInvoice("i3", "o2", 10000)]
        );

        var summary = svc.GetOrganizationSummary("o1");

        Assert.Equal(2, summary.UserCount);
        Assert.Equal(2, summary.ProjectCount);
        Assert.Equal(8000m, summary.TotalInvoiced);
    }

    [Fact]
    public void GetOrganizationSummary_ExcludesDataFromOtherOrgs()
    {
        var svc = CreateService(
            users: [MakeUser("u1", "o1"), MakeUser("u2", "o2")],
            projects: [MakeProject("p1", "o1"), MakeProject("p2", "o2")],
            invoices: [MakeInvoice("i1", "o1", 5000), MakeInvoice("i2", "o2", 10000)]
        );

        var summary = svc.GetOrganizationSummary("o2");

        Assert.Equal(1, summary.UserCount);
        Assert.Equal(1, summary.ProjectCount);
        Assert.Equal(10000m, summary.TotalInvoiced);
    }

    [Fact]
    public void GetOrganizationSummary_WithNoRelatedData_ReturnsZeros()
    {
        var svc = CreateService(
            users: [MakeUser("u1", "other-org")],
            projects: [MakeProject("p1", "other-org")],
            invoices: [MakeInvoice("i1", "other-org", 5000)]
        );

        var summary = svc.GetOrganizationSummary("o1");

        Assert.Equal(0, summary.UserCount);
        Assert.Equal(0, summary.ProjectCount);
        Assert.Equal(0m, summary.TotalInvoiced);
    }

    // ── Total Hours Logged ──────────────────────────────────────────

    [Fact]
    public void GetTotalHoursLogged_SumsHoursForProject()
    {
        var svc = CreateService(timeEntries:
        [
            MakeTimeEntry("te1", "p1", 4),
            MakeTimeEntry("te2", "p1", 8),
            MakeTimeEntry("te3", "p1", 2)
        ]);

        Assert.Equal(14, svc.GetTotalHoursLogged("p1"));
    }

    [Fact]
    public void GetTotalHoursLogged_OnlyIncludesEntriesForSpecifiedProject()
    {
        var svc = CreateService(timeEntries:
        [
            MakeTimeEntry("te1", "p1", 4),
            MakeTimeEntry("te2", "p2", 10),
            MakeTimeEntry("te3", "p1", 6),
            MakeTimeEntry("te4", "p3", 20)
        ]);

        Assert.Equal(10, svc.GetTotalHoursLogged("p1"));
    }

    [Fact]
    public void GetTotalHoursLogged_WithNoEntries_ReturnsZero()
    {
        var svc = CreateService();

        Assert.Equal(0, svc.GetTotalHoursLogged("nonexistent"));
    }
}
