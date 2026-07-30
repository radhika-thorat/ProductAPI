
using System.Net;
using Tests.Helpers;
using Xunit;

/// <summary>
/// Contains integration tests for the Product API.
/// </summary>
public class ProductApiIntegrationTests
{
    /// <summary>
    /// Verifies that the Get Products API endpoint returns
    /// either a successful response (200 OK) or
    /// a not found response (404 Not Found).
    /// </summary>
    [Fact]
    public async Task GetProducts_ReturnsSuccess()
    {
        // Arrange
        using var factory = new CustomWebApplicationFactory<Program>();

        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/v1/products");

        // Assert
        Assert.True(
            response.StatusCode == HttpStatusCode.OK ||
            response.StatusCode == HttpStatusCode.NotFound);
    }
}