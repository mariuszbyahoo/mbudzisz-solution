using System.Net;
using System.Text.Json;
using MBudziszSolution.Tests.Integration.Fixtures;

namespace MBudziszSolution.Tests.Integration.Endpoints;

public class UsersEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public UsersEndpointTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    // ── GET /api/users ──────────────────────────────────────────────

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/users");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAll_ReturnsJsonContentType()
    {
        var response = await _client.GetAsync("/api/users");

        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task GetAll_ReturnsAllUsers()
    {
        var response = await _client.GetAsync("/api/users");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var root = json.RootElement;

        Assert.Equal(JsonValueKind.Array, root.ValueKind);
        Assert.Equal(8, root.GetArrayLength());
    }

    [Fact]
    public async Task GetAll_FilterByOrgId_ReturnsOnlyMatchingUsers()
    {
        var response = await _client.GetAsync("/api/users?orgId=org-001");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var items = json.RootElement;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(3, items.GetArrayLength());

        foreach (var item in items.EnumerateArray())
            Assert.Equal("org-001", item.GetProperty("orgId").GetString());
    }

    [Fact]
    public async Task GetAll_FilterByRole_ReturnsOnlyMatchingUsers()
    {
        var response = await _client.GetAsync("/api/users?role=admin");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var items = json.RootElement;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(4, items.GetArrayLength());

        foreach (var item in items.EnumerateArray())
            Assert.Equal("admin", item.GetProperty("role").GetString());
    }

    [Fact]
    public async Task GetAll_FilterByActive_ReturnsOnlyMatchingUsers()
    {
        var response = await _client.GetAsync("/api/users?active=true");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var items = json.RootElement;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(7, items.GetArrayLength());

        foreach (var item in items.EnumerateArray())
            Assert.True(item.GetProperty("active").GetBoolean());
    }

    // ── GET /api/users/{id} ─────────────────────────────────────────

    [Fact]
    public async Task GetById_WithValidId_ReturnsUserContract()
    {
        var response = await _client.GetAsync("/api/users/user-001");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var root = json.RootElement;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(JsonValueKind.Object, root.ValueKind);
        Assert.Equal("user-001", root.GetProperty("id").GetString());
        Assert.Equal("org-001", root.GetProperty("orgId").GetString());
        Assert.Equal("Alice Chen", root.GetProperty("name").GetString());
        Assert.Equal("alice@acme.com", root.GetProperty("email").GetString());
        Assert.Equal("admin", root.GetProperty("role").GetString());
        Assert.True(root.GetProperty("active").GetBoolean());
        Assert.True(root.TryGetProperty("bio", out _), "Missing field: bio");
    }

    [Fact]
    public async Task GetById_WithInvalidId_Returns404WithErrorBody()
    {
        var response = await _client.GetAsync("/api/users/nonexistent-id");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("User not found", json.RootElement.GetProperty("error").GetString());
    }
}
