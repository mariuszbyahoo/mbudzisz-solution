using System.Net;
using System.Text.Json;
using MBudziszSolution.Tests.Integration.Fixtures;

namespace MBudziszSolution.Tests.Integration.Endpoints;

public class ProjectsEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ProjectsEndpointTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    // ── GET /api/projects ───────────────────────────────────────────

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/projects");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAll_ReturnsJsonContentType()
    {
        var response = await _client.GetAsync("/api/projects");

        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task GetAll_ReturnsAllProjects()
    {
        var response = await _client.GetAsync("/api/projects");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var root = json.RootElement;

        Assert.Equal(JsonValueKind.Array, root.ValueKind);
        Assert.Equal(6, root.GetArrayLength());
    }

    [Fact]
    public async Task GetAll_FilterByOrgId_ReturnsOnlyMatchingProjects()
    {
        var response = await _client.GetAsync("/api/projects?orgId=org-001");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var items = json.RootElement;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(3, items.GetArrayLength());

        foreach (var item in items.EnumerateArray())
            Assert.Equal("org-001", item.GetProperty("orgId").GetString());
    }

    [Fact]
    public async Task GetAll_FilterByStatus_ReturnsOnlyMatchingProjects()
    {
        var response = await _client.GetAsync("/api/projects?status=active");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var items = json.RootElement;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(4, items.GetArrayLength());

        foreach (var item in items.EnumerateArray())
            Assert.Equal("active", item.GetProperty("status").GetString());
    }

    // ── GET /api/projects/{id} ──────────────────────────────────────

    [Fact]
    public async Task GetById_WithValidId_ReturnsProjectDetailContract()
    {
        var response = await _client.GetAsync("/api/projects/proj-001");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var root = json.RootElement;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(JsonValueKind.Object, root.ValueKind);

        // Core project fields
        Assert.Equal("proj-001", root.GetProperty("id").GetString());
        Assert.Equal("org-001", root.GetProperty("orgId").GetString());
        Assert.Equal("Platform Modernization", root.GetProperty("name").GetString());
        Assert.Equal("active", root.GetProperty("status").GetString());
        Assert.Equal(800, root.GetProperty("budgetHours").GetInt32());
        Assert.Equal("2024-01-01", root.GetProperty("startDate").GetString());
        Assert.Equal("2024-06-30", root.GetProperty("endDate").GetString());
        Assert.True(root.TryGetProperty("description", out _), "Missing field: description");

        // Embedded organization object
        var org = root.GetProperty("organization");
        Assert.Equal(JsonValueKind.Object, org.ValueKind);
        Assert.Equal("org-001", org.GetProperty("id").GetString());
        Assert.Equal("Acme Corp", org.GetProperty("name").GetString());

        // Computed aggregate: total hours from time entries for proj-001
        Assert.Equal(35, root.GetProperty("totalHoursLogged").GetInt32());
    }

    [Fact]
    public async Task GetById_WithInvalidId_Returns404WithErrorBody()
    {
        var response = await _client.GetAsync("/api/projects/nonexistent-id");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("Project not found", json.RootElement.GetProperty("error").GetString());
    }
}
