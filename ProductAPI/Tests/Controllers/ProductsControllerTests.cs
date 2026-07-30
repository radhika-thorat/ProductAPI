using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductSolution.ProductAPI.Controllers;
using ProductSolution.ProductApplication.DTOs;
using ProductSolution.ProductApplication.Interfaces;
using Xunit;

/// <summary>
/// Contains unit tests for the <see cref="ProductsController"/> class.
/// </summary>
public class ProductsControllerTests
{
    /// <summary>
    /// Mock instance of the product service.
    /// </summary>
    private readonly Mock<IProductService> _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductsControllerTests"/> class.
    /// </summary>
    public ProductsControllerTests()
    {
        _service = new Mock<IProductService>();
    }

    /// <summary>
    /// Verifies that the GetAll action returns an <see cref="OkObjectResult"/>
    /// when products are available.
    /// </summary>
    [Fact]
    public async Task GetAll_Returns_Ok()
    {
        // Arrange
        _service.Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<ProductDto>
            {
                new ProductDto
                {
                    Id = 1,
                    ProductName = "Laptop"
                }
            });

        var controller = new ProductsController(_service.Object);

        // Act
        var result = await controller.GetAll();

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    /// <summary>
    /// Verifies that the GetById action returns an <see cref="OkObjectResult"/>
    /// when a valid product ID is provided.
    /// </summary>
    [Fact]
    public async Task GetById_Returns_Ok()
    {
        // Arrange
        _service.Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(new ProductDto
            {
                Id = 1,
                ProductName = "Laptop"
            });

        var controller = new ProductsController(_service.Object);

        // Act
        var result = await controller.GetById(1);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }
}