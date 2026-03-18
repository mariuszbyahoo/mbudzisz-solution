using System.Net;
using System.Text.Json;
using MBudziszSolution.Tests.Integration.Fixtures;

namespace MBudziszSolution.Tests.Integration.Endpoints;

public class InvoicesEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public InvoicesEndpointTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    // ── GET /api/invoices ───────────────────────────────────────────

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/invoices");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAll_ReturnsJsonContentType()
    {
        var response = await _client.GetAsync("/api/invoices");

        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task GetAll_ReturnsAllInvoices()
    {
        var response = await _client.GetAsync("/api/invoices");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var root = json.RootElement;

        Assert.Equal(JsonValueKind.Array, root.ValueKind);
        Assert.Equal(4, root.GetArrayLength());

        // Verify field names on first entry
        var first = root[0];
        Assert.True(first.TryGetProperty("id", out _), "Missing field: id");
        Assert.True(first.TryGetProperty("orgId", out _), "Missing field: orgId");
        Assert.True(first.TryGetProperty("projectId", out _), "Missing field: projectId");
        Assert.True(first.TryGetProperty("amount", out _), "Missing field: amount");
        Assert.True(first.TryGetProperty("currency", out _), "Missing field: currency");
        Assert.True(first.TryGetProperty("status", out _), "Missing field: status");
        Assert.True(first.TryGetProperty("description", out _), "Missing field: description");
    }

    [Fact]
    public async Task GetAll_FilterByOrgId_ReturnsOnlyMatchingInvoices()
    {
        var response = await _client.GetAsync("/api/invoices?orgId=org-001");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var items = json.RootElement;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(2, items.GetArrayLength());

        foreach (var item in items.EnumerateArray())
            Assert.Equal("org-001", item.GetProperty("orgId").GetString());
    }

    [Fact]
    public async Task GetAll_FilterByStatus_ReturnsOnlyMatchingInvoices()
    {
        var response = await _client.GetAsync("/api/invoices?status=sent");
        var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var items = json.RootElement;

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(2, items.GetArrayLength());

        foreach (var item in items.EnumerateArray())
            Assert.Equal("sent", item.GetProperty("status").GetString());
    }
}
