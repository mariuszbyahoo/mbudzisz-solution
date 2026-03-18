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
        // Act
        var response = await _client.GetAsync("/api/dashboard");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetDashboard_ReturnsJsonContentType()
    {
        // Act
        var response = await _client.GetAsync("/api/dashboard");

        // Assert
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task GetDashboard_ReturnsJsonObject()
    {
        // Act
        var response = await _client.GetAsync("/api/dashboard");
        var content = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(content);

        // Assert
        Assert.Equal(JsonValueKind.Object, json.RootElement.ValueKind);
    }
}
