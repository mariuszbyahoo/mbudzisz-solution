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

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/api/users");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAll_ReturnsJsonContentType()
    {
        // Act
        var response = await _client.GetAsync("/api/users");

        // Assert
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task GetAll_ReturnsJsonArray()
    {
        // Act
        var response = await _client.GetAsync("/api/users");
        var content = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(content);

        // Assert
        Assert.Equal(JsonValueKind.Array, json.RootElement.ValueKind);
    }

    [Fact]
    public async Task GetAll_FilterByOrgId_ReturnsOkWithJsonArray()
    {
        // Act
        var response = await _client.GetAsync("/api/users?orgId=org-001");
        var content = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(content);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(JsonValueKind.Array, json.RootElement.ValueKind);
    }

    [Fact]
    public async Task GetAll_FilterByRole_ReturnsOkWithJsonArray()
    {
        // Act
        var response = await _client.GetAsync("/api/users?role=admin");
        var content = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(content);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(JsonValueKind.Array, json.RootElement.ValueKind);
    }

    [Fact]
    public async Task GetAll_FilterByActive_ReturnsOkWithJsonArray()
    {
        // Act
        var response = await _client.GetAsync("/api/users?active=true");
        var content = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(content);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(JsonValueKind.Array, json.RootElement.ValueKind);
    }

    [Fact]
    public async Task GetById_WithValidId_ReturnsOk()
    {
        // Act
        var response = await _client.GetAsync("/api/users/user-001");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetById_WithValidId_ReturnsJsonObject()
    {
        // Act
        var response = await _client.GetAsync("/api/users/user-001");
        var content = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(content);

        // Assert
        Assert.Equal(JsonValueKind.Object, json.RootElement.ValueKind);
    }

    [Fact]
    public async Task GetById_WithInvalidId_ReturnsNotFound()
    {
        // Act
        var response = await _client.GetAsync("/api/users/nonexistent-id");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
