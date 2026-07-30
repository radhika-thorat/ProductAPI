using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProductSolution.Infrastructure.Repositories;
using ProductSolution.ProductDomain.Entities;
using Xunit;

/// <summary>
/// Contains unit tests for the <see cref="ProductRepository"/> class.
/// </summary>
public class ProductRepositoryTests
{
    /// <summary>
    /// Verifies that a product is successfully saved to the repository
    /// and can be retrieved with the correct details.
    /// </summary>
    [Fact]
    public async Task Add_Product_Should_Save()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new ApplicationDbContext(options);

        var repository = new ProductRepository(context);

        // Act
        await repository.AddAsync(
            new Product
            {
                ProductName = "Laptop",
                CreatedBy = "Admin",
                CreatedOn = DateTime.UtcNow
            },
            10);

        // Assert
        var result = await repository.GetAllAsync();

        result.Should().HaveCount(1);
        result.First().ProductName.Should().Be("Laptop");
        result.First().Quantity.Should().Be(10);
    }
}