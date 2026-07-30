namespace ProductSolution.ProductApplication.Interfaces;

using ProductSolution.ProductApplication.DTOs;

/// <summary>
/// Defines business operations for managing products.
/// </summary>
public interface IProductService
{
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
    /// Creates a new product.
    /// </summary>
    /// <param name="dto">
    /// The product details required to create a new product.
    /// </param>
    /// <returns>
    /// The newly created <see cref="ProductDto"/>.
    /// </returns>
    Task<ProductDto> CreateAsync(CreateProductDto dto);

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the product.
    /// </param>
    /// <param name="dto">
    /// The updated product details.
    /// </param>
    Task UpdateAsync(int id, UpdateProductDto dto);

    /// <summary>
    /// Deletes a product by its unique identifier.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the product.
    /// </param>
    Task DeleteAsync(int id);

    /// <summary>
    /// Retrieves products using pagination.
    /// </summary>
    /// <param name="pageNumber">
    /// The page number to retrieve.
    /// </param>
    /// <param name="pageSize">
    /// The number of records to retrieve per page.
    /// </param>
    /// <returns>
    /// A paginated collection of <see cref="ProductDto"/>.
    /// </returns>
    Task<IEnumerable<ProductDto>> GetPagedAsync(int pageNumber, int pageSize);
}