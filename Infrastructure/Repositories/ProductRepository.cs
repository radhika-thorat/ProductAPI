using Microsoft.EntityFrameworkCore;
using ProductSolution.ProductApplication.DTOs;
using ProductSolution.ProductApplication.Interfaces;
using ProductSolution.ProductDomain.Entities;
using ProductSolution.ProductDomain.Exceptions;

namespace ProductSolution.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for managing product and item data.
/// Provides CRUD operations and data retrieval methods.
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProductRepository"/> class.
    /// </summary>
    /// <param name="context">
    /// Database context used to access Product and Item tables.
    /// </param>
    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all products with their corresponding item quantity.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="ProductDto"/>.
    /// </returns>
    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        return await (
            from p in _context.Products
            join i in _context.Items
                on p.Id equals i.ProductId
            select new ProductDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Quantity = i.Quantity,
                CreatedBy = p.CreatedBy,
                CreatedOn = p.CreatedOn,
                ModifiedBy = p.ModifiedBy,
                ModifiedOn = p.ModifiedOn
            })
            .AsNoTracking()
            .ToListAsync();
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
        return await (
            from p in _context.Products
            join i in _context.Items
                on p.Id equals i.ProductId
            where p.Id == id
            select new ProductDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Quantity = i.Quantity,
                CreatedBy = p.CreatedBy,
                CreatedOn = p.CreatedOn,
                ModifiedBy = p.ModifiedBy,
                ModifiedOn = p.ModifiedOn
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();
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
    /// A paginated collection of <see cref="Product"/>.
    /// </returns>
    public async Task<IEnumerable<Product>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return await _context.Products
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    /// <summary>
    /// Creates a new product and its associated item quantity.
    /// </summary>
    /// <param name="product">
    /// Product entity to create.
    /// </param>
    /// <param name="quantity">
    /// Initial quantity for the product.
    /// </param>
    /// <returns>
    /// The created <see cref="ProductDto"/>.
    /// </returns>
    public async Task<ProductDto> AddAsync(Product product, int quantity)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        await _context.Items.AddAsync(new Item
        {
            ProductId = product.Id,
            Quantity = quantity
        });

        await _context.SaveChangesAsync();

        return await GetByIdAsync(product.Id)
            ?? throw new Exception("Unable to retrieve created product.");
    }

    /// <summary>
    /// Updates an existing product and its quantity.
    /// </summary>
    /// <param name="productId">
    /// Product identifier.
    /// </param>
    /// <param name="dto">
    /// Updated product information.
    /// </param>
    public async Task UpdateAsync(int productId, UpdateProductDto dto)
    {
        var product = await _context.Products.FindAsync(productId);

        if (product == null)
            throw new NotFoundException($"Product with Id {productId} not found.");

        product.ProductName = dto.ProductName;
        product.ModifiedBy = "Admin";
        product.ModifiedOn = DateTime.UtcNow;

        var item = await _context.Items
            .FirstOrDefaultAsync(x => x.ProductId == productId);

        if (item != null)
        {
            item.Quantity = dto.Quantity;
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a product and its associated item.
    /// </summary>
    /// <param name="productId">
    /// Product identifier.
    /// </param>
    public async Task DeleteAsync(int productId)
    {
        var product = await _context.Products.FindAsync(productId);

        if (product == null)
            throw new NotFoundException($"Product with Id {productId} not found.");

        var item = await _context.Items
            .FirstOrDefaultAsync(x => x.ProductId == productId);

        if (item != null)
        {
            _context.Items.Remove(item);
        }

        _context.Products.Remove(product);

        await _context.SaveChangesAsync();
    }
}