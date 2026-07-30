namespace ProductSolution.ProductApplication.Interfaces;

using ProductDomain.Entities;
using ProductSolution.ProductApplication.DTOs;

/// <summary>
/// Defines repository operations for managing product data.
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Adds a new product along with its quantity.
    /// </summary>
    /// <param name="product">
    /// The product entity to be created.
    /// </param>
    /// <param name="quantity">
    /// The initial quantity of the product.
    /// </param>
    /// <returns>
    /// The created <see cref="ProductDto"/>.
    /// </returns>
    Task<ProductDto> AddAsync(Product product, int quantity);

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="productId">
    /// The unique identifier of the product.
    /// </param>
    /// <param name="dto">
    /// The updated product information.
    /// </param>
    Task UpdateAsync(int productId, UpdateProductDto dto);

    /// <summary>
    /// Deletes a product by its unique identifier.
    /// </summary>
    /// <param name="productId">
    /// The unique identifier of the product.
    /// </param>
    Task DeleteAsync(int productId);

    /// <summary>
    /// Retrieves all products.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="ProductDto"/>.
    /// </returns>
    Task<IEnumerable<ProductDto>> GetAllAsync();

    /// <summary>
    /// Retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the product.
    /// </param>
    /// <returns>
    /// A <see cref="ProductDto"/> if found; otherwise, null.
    /// </returns>
    Task<ProductDto?> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves products using pagination.
    /// </summary>
    /// <param name="pageNumber">
    /// The page number to retrieve.
    /// </param>
    /// <param name="pageSize">
    /// The number of records per page.
    /// </param>
    /// <returns>
    /// A paginated collection of <see cref="Product"/>.
    /// </returns>
    Task<IEnumerable<Product>> GetPagedAsync(int pageNumber, int pageSize);
}