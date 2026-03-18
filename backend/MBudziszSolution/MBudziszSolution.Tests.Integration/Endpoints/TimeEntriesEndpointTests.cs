using System.Net;
using System.Text.Json;
using MBudziszSolution.Tests.Integration.Fixtures;

namespace MBudziszSolution.Tests.Integration.Endpoints;

public class TimeEntriesEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public TimeEntriesEndpointTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    // ── GET /api/time-entries ───────────────────────────────────────

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/time-entries");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAll_ReturnsJsonContentType()
    {
        var response = await _client.GetAsync("/api/time-entries");

        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task GetAll_ReturnsAllTimeEntries()
    {
        var response = await _client.GetAsync("/api/time-entries");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var root = json.RootElement;

        Assert.Equal(JsonValueKind.Array, root.ValueKind);
        Assert.Equal(12, root.GetArrayLength());

        // Verify field names on first entry
        var first = root[0];
        Assert.True(first.TryGetProperty("id", out _), "Missing field: id");
        Assert.True(first.TryGetProperty("userId", out _), "Missing field: userId");
        Assert.True(first.TryGetProperty("projectId", out _), "Missing field: projectId");
        Assert.True(first.TryGetProperty("date", out _), "Missing field: date");
        Assert.True(first.TryGetProperty("hours", out _), "Missing field: hours");
        Assert.True(first.TryGetProperty("description", out _), "Missing field: description");
    }

    [Fact]
    public async Task GetAll_FilterByUserId_ReturnsOnlyMatchingEntries()
    {
        var response = await _client.GetAsync("/api/time-entries?userId=user-001");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var items = json.RootElement;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(4, items.GetArrayLength());

        foreach (var item in items.EnumerateArray())
            Assert.Equal("user-001", item.GetProperty("userId").GetString());
    }

    [Fact]
    public async Task GetAll_FilterByProjectId_ReturnsOnlyMatchingEntries()
    {
        var response = await _client.GetAsync("/api/time-entries?projectId=proj-001");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var items = json.RootElement;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(6, items.GetArrayLength());

        foreach (var item in items.EnumerateArray())
            Assert.Equal("proj-001", item.GetProperty("projectId").GetString());
    }

    [Fact]
    public async Task GetAll_FilterByDateRange_ReturnsOnlyMatchingEntries()
    {
        // All seed entries fall within March 2024, so this range returns all 12
        var response = await _client.GetAsync("/api/time-entries?from=2024-03-01&to=2024-03-31");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var items = json.RootElement;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(12, items.GetArrayLength());
    }
}
