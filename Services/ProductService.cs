using AutoMapper;
using ProductSolution.ProductApplication.DTOs;
using ProductSolution.ProductApplication.Interfaces;
using ProductSolution.ProductDomain.Entities;

namespace ProductSolution.Services;

/// <summary>
/// Provides business logic for managing products.
/// </summary>
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductService"/> class.
    /// </summary>
    /// <param name="productRepository">
    /// Repository used for product data access operations.
    /// </param>
    /// <param name="unitOfWork">
    /// Unit of Work used for transaction management.
    /// </param>
    /// <param name="mapper">
    /// AutoMapper instance used for object mapping.
    /// </param>
    public ProductService(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves all products.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="ProductDto"/>.
    /// </returns>
    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="id">
    /// Product identifier.
    /// </param>
    /// <returns>
    /// A <see cref="ProductDto"/> if found; otherwise, null.
    /// </returns>
    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="dto">
    /// Product information required to create a new product.
    /// </param>
    /// <returns>
    /// The newly created <see cref="ProductDto"/>.
    /// </returns>
    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            ProductName = dto.ProductName,
            CreatedBy = "Admin",
            CreatedOn = DateTime.UtcNow
        };

        return await _productRepository.AddAsync(product, dto.Quantity);
    }

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="id">
    /// Product identifier.
    /// </param>
    /// <param name="dto">
    /// Updated product information.
    /// </param>
    public async Task UpdateAsync(int id, UpdateProductDto dto)
    {
        await _productRepository.UpdateAsync(id, dto);
    }

    /// <summary>
    /// Deletes a product.
    /// </summary>
    /// <param name="id">
    /// Product identifier.
    /// </param>
    public async Task DeleteAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }

    /// <summary>
    /// Retrieves products using pagination.
    /// </summary>
    /// <param name="pageNumber">
    /// Current page number.
    /// </param>
    /// <param name="pageSize">
    /// Number of records per page.
    /// </param>
    /// <returns>
    /// A paginated collection of <see cref="ProductDto"/>.
    /// </returns>
    public async Task<IEnumerable<ProductDto>> GetPagedAsync(int pageNumber, int pageSize)
    {
        var products = await _productRepository.GetPagedAsync(pageNumber, pageSize);

        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
}