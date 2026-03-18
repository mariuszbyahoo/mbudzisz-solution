using System.Net;
using System.Text.Json;
using MBudziszSolution.Tests.Integration.Fixtures;

namespace MBudziszSolution.Tests.Integration.Endpoints;

public class OrganizationsEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public OrganizationsEndpointTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    // ── GET /api/organizations ──────────────────────────────────────

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/organizations");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAll_ReturnsJsonContentType()
    {
        var response = await _client.GetAsync("/api/organizations");

        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task GetAll_ReturnsAllOrganizations()
    {
        var response = await _client.GetAsync("/api/organizations");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var root = json.RootElement;

        Assert.Equal(JsonValueKind.Array, root.ValueKind);
        Assert.Equal(4, root.GetArrayLength());
    }

    [Fact]
    public async Task GetAll_FilterByTier_ReturnsOnlyMatchingOrganizations()
    {
        var response = await _client.GetAsync("/api/organizations?tier=enterprise");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var items = json.RootElement;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(2, items.GetArrayLength());

        foreach (var item in items.EnumerateArray())
            Assert.Equal("enterprise", item.GetProperty("tier").GetString());
    }

    [Fact]
    public async Task GetAll_FilterByIndustry_ReturnsOnlyMatchingOrganizations()
    {
        var response = await _client.GetAsync("/api/organizations?industry=Healthcare");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var items = json.RootElement;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(1, items.GetArrayLength());
        Assert.Equal("org-003", items[0].GetProperty("id").GetString());
    }

    // ── GET /api/organizations/{id} ─────────────────────────────────

    [Fact]
    public async Task GetById_WithValidId_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/organizations/org-001");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetById_WithValidId_ReturnsOrganizationContract()
    {
        var response = await _client.GetAsync("/api/organizations/org-001");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var root = json.RootElement;

        Assert.Equal(JsonValueKind.Object, root.ValueKind);
        Assert.Equal("org-001", root.GetProperty("id").GetString());
        Assert.Equal("Acme Corp", root.GetProperty("name").GetString());
        Assert.Equal("acme-corp", root.GetProperty("slug").GetString());
        Assert.Equal("Technology", root.GetProperty("industry").GetString());
        Assert.Equal("enterprise", root.GetProperty("tier").GetString());
        Assert.Equal("partnerships@acme.com", root.GetProperty("contactEmail").GetString());
        Assert.True(root.TryGetProperty("description", out _), "Missing field: description");
        Assert.True(root.TryGetProperty("settings", out _), "Missing field: settings");
        Assert.True(root.TryGetProperty("metadata", out _), "Missing field: metadata");
    }

    [Fact]
    public async Task GetById_WithInvalidId_Returns404WithErrorBody()
    {
        var response = await _client.GetAsync("/api/organizations/nonexistent-id");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("Organization not found", json.RootElement.GetProperty("error").GetString());
    }

    // ── GET /api/organizations/{id}/summary ─────────────────────────

    [Fact]
    public async Task GetSummary_WithValidId_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/organizations/org-001/summary");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetSummary_WithValidId_ReturnsComputedAggregates()
    {
        var response = await _client.GetAsync("/api/organizations/org-001/summary");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var root = json.RootElement;

        Assert.Equal(JsonValueKind.Object, root.ValueKind);

        // Embedded organization object
        var org = root.GetProperty("organization");
        Assert.Equal("org-001", org.GetProperty("id").GetString());
        Assert.Equal("Acme Corp", org.GetProperty("name").GetString());

        // Computed aggregates from seed data
        Assert.Equal(3, root.GetProperty("userCount").GetInt32());
        Assert.Equal(3, root.GetProperty("projectCount").GetInt32());
        Assert.Equal(57000m, root.GetProperty("totalInvoiced").GetDecimal());
        Assert.Equal("USD", root.GetProperty("currency").GetString());
    }

    [Fact]
    public async Task GetSummary_WithInvalidId_Returns404WithErrorBody()
    {
        var response = await _client.GetAsync("/api/organizations/nonexistent-id/summary");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("Organization not found", json.RootElement.GetProperty("error").GetString());
    }
}
