using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductSolution.ProductApplication.DTOs;
using ProductSolution.ProductApplication.Interfaces;

namespace ProductSolution.ProductAPI.Controllers
{
    /// <summary>
    /// Controller for managing product operations.
    /// Provides endpoints to retrieve, create, update, and delete products.
    /// </summary>
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="service">
        /// Product service used to perform business operations.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the product service is null.
        /// </exception>
        public ProductsController(IProductService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>
        /// Returns a list of all available products.
        /// </returns>
        /// <response code="200">Returns the list of products.</response>
        /// <response code="401">User is not authorized.</response>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the product.
        /// </param>
        /// <returns>
        /// Returns the requested product.
        /// </returns>
        /// <response code="200">Returns the requested product.</response>
        /// <response code="404">Product not found.</response>
        /// <response code="401">User is not authorized.</response>
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="dto">
        /// Product details required to create a new product.
        /// </param>
        /// <returns>
        /// Returns the newly created product.
        /// </returns>
        /// <response code="201">Product created successfully.</response>
        /// <response code="400">Invalid request data.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="403">Only Admin users can create products.</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var result = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id, version = "1.0" },
                result);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the product.
        /// </param>
        /// <param name="dto">
        /// Updated product details.
        /// </param>
        /// <returns>
        /// Returns a success message after updating the product.
        /// </returns>
        /// <response code="200">Product updated successfully.</response>
        /// <response code="400">Invalid request data.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="403">Only Admin users can update products.</response>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateProductDto dto)
        {
            await _service.UpdateAsync(id, dto);

            return Ok(new
            {
                Message = "Product updated successfully."
            });
        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the product.
        /// </param>
        /// <returns>
        /// Returns a success message after deleting the product.
        /// </returns>
        /// <response code="200">Product deleted successfully.</response>
        /// <response code="401">User is not authorized.</response>
        /// <response code="403">Only Admin users can delete products.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return Ok(new
            {
                Message = "Product deleted successfully."
            });
        }
    }
}