using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ProductApplication.DTOs;

namespace ProductAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Returns all products.
        /// </summary>
        /// <returns>List of products.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        /// <summary>
        /// Returns a product by id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var result = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById),
                new { id = result.Id, version = "1.0" },
                result);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        [HttpPut("{id:int}")]
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
        [HttpDelete("{id:int}")]
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
