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

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/api/time-entries");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAll_ReturnsJsonContentType()
    {
        // Act
        var response = await _client.GetAsync("/api/time-entries");

        // Assert
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task GetAll_ReturnsJsonArray()
    {
        // Act
        var response = await _client.GetAsync("/api/time-entries");
        var content = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(content);

        // Assert
        Assert.Equal(JsonValueKind.Array, json.RootElement.ValueKind);
    }

    [Fact]
    public async Task GetAll_FilterByUserId_ReturnsOkWithJsonArray()
    {
        // Act
        var response = await _client.GetAsync("/api/time-entries?userId=user-001");
        var content = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(content);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(JsonValueKind.Array, json.RootElement.ValueKind);
    }

    [Fact]
    public async Task GetAll_FilterByProjectId_ReturnsOkWithJsonArray()
    {
        // Act
        var response = await _client.GetAsync("/api/time-entries?projectId=proj-001");
        var content = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(content);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(JsonValueKind.Array, json.RootElement.ValueKind);
    }

    [Fact]
    public async Task GetAll_FilterByDateRange_ReturnsOkWithJsonArray()
    {
        // Act
        var response = await _client.GetAsync("/api/time-entries?from=2024-03-01&to=2024-03-31");
        var content = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(content);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(JsonValueKind.Array, json.RootElement.ValueKind);
    }
}
