using AutoMapper;
using FluentAssertions;
using Moq;
using ProductSolution.ProductApplication.DTOs;
using ProductSolution.ProductApplication.Interfaces;
using ProductSolution.Services;
using Xunit;

/// <summary>
/// Contains unit tests for the <see cref="ProductService"/> class.
/// </summary>
public class ProductServiceTests
{
    /// <summary>
    /// Mock instance of the product repository.
    /// </summary>
    private readonly Mock<IProductRepository> _repository;

    /// <summary>
    /// Mock instance of the unit of work.
    /// </summary>
    private readonly Mock<IUnitOfWork> _unitOfWork;

    /// <summary>
    /// AutoMapper instance used for mapping objects during testing.
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductServiceTests"/> class.
    /// Configures the required mocks and AutoMapper profile.
    /// </summary>
    public ProductServiceTests()
    {
        _repository = new Mock<IProductRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        _mapper = config.CreateMapper();
    }

    /// <summary>
    /// Verifies that the <see cref="ProductService.CreateAsync(CreateProductDto)"/>
    /// method creates and returns a product successfully.
    /// </summary>
    [Fact]
    public async Task CreateProduct_ShouldReturnProduct()
    {
        // Arrange
        var service = new ProductService(
            _repository.Object,
            _unitOfWork.Object,
            _mapper);

        var dto = new CreateProductDto
        {
            ProductName = "Laptop"
        };

        // Act
        var result = await service.CreateAsync(dto);

        // Assert
        result.Should().NotBeNull();
        result.ProductName.Should().Be("Laptop");
    }
}