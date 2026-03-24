using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Xunit;

public class OrdersIntegrationTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public OrdersIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_Orders_Should_Return_Accepted()
    {
        // Arrange
        var request = new
        {
            items = new[]
            {
                new { name = "Burger", area = 1 },
                new { name = "Fries", area = 0 }
            }
        };

        // Act
        var response = await _client.PostAsJsonAsync("/orders", request);

        // Assert
        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
    }

    [Fact]
    public async Task Post_Orders_With_Empty_Items_Should_Return_BadRequest()
    {
        // Arrange
        var request = new { items = Array.Empty<object>() };

        // Act
        var response = await _client.PostAsJsonAsync("/orders", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Post_Orders_With_Invalid_Item_Name_Should_Return_BadRequest()
    {
        // Arrange
        var request = new
        {
            items = new[]
            {
                new { name = "", area = 1 }
            }
        };

        // Act
        var response = await _client.PostAsJsonAsync("/orders", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
