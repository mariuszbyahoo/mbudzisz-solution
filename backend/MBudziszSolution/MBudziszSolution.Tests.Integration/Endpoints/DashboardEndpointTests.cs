using System.Net;
using System.Text.Json;
using MBudziszSolution.Tests.Integration.Fixtures;

namespace MBudziszSolution.Tests.Integration.Endpoints;

public class DashboardEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public DashboardEndpointTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetDashboard_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/dashboard");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetDashboard_ReturnsJsonContentType()
    {
        var response = await _client.GetAsync("/api/dashboard");

        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
    }

    /// <summary>
    /// Pins the exact camelCase field names the Angular frontend consumes.
    /// If the backend renames a field, this test will fail immediately
    /// instead of silently breaking the UI.
    /// </summary>
    [Fact]
    public async Task GetDashboard_ReturnsExpectedFieldNames()
    {
        var response = await _client.GetAsync("/api/dashboard");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var root = json.RootElement;

        Assert.True(root.TryGetProperty("totalOrganizations", out _), "Missing field: totalOrganizations");
        Assert.True(root.TryGetProperty("totalUsers", out _), "Missing field: totalUsers");
        Assert.True(root.TryGetProperty("totalProjects", out _), "Missing field: totalProjects");
        Assert.True(root.TryGetProperty("activeProjects", out _), "Missing field: activeProjects");
        Assert.True(root.TryGetProperty("totalTimeEntries", out _), "Missing field: totalTimeEntries");
        Assert.True(root.TryGetProperty("totalInvoiced", out _), "Missing field: totalInvoiced");
    }

    /// <summary>
    /// Guards against accidental renames by asserting the old field names are absent.
    /// </summary>
    [Fact]
    public async Task GetDashboard_DoesNotReturnLegacyFieldNames()
    {
        var response = await _client.GetAsync("/api/dashboard");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var root = json.RootElement;

        Assert.False(root.TryGetProperty("organizationCount", out _), "Legacy field 'organizationCount' must not be present");
        Assert.False(root.TryGetProperty("userCount", out _), "Legacy field 'userCount' must not be present");
        Assert.False(root.TryGetProperty("projectCount", out _), "Legacy field 'projectCount' must not be present");
        Assert.False(root.TryGetProperty("timeEntryCount", out _), "Legacy field 'timeEntryCount' must not be present");
        Assert.False(root.TryGetProperty("activeProjectCount", out _), "Legacy field 'activeProjectCount' must not be present");
    }

    /// <summary>
    /// Verifies computed values match the in-memory seed data so a SeedData
    /// change is immediately visible here rather than through UI debugging.
    /// </summary>
    [Fact]
    public async Task GetDashboard_ReturnsCorrectValuesFromSeedData()
    {
        var response = await _client.GetAsync("/api/dashboard");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var root = json.RootElement;

        Assert.Equal(4, root.GetProperty("totalOrganizations").GetInt32());
        Assert.Equal(8, root.GetProperty("totalUsers").GetInt32());
        Assert.Equal(6, root.GetProperty("totalProjects").GetInt32());
        Assert.Equal(4, root.GetProperty("activeProjects").GetInt32());
        Assert.Equal(12, root.GetProperty("totalTimeEntries").GetInt32());
        Assert.Equal(137000m, root.GetProperty("totalInvoiced").GetDecimal());
    }
}
