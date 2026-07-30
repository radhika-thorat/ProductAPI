namespace ProductSolution.ProductApplication.DTOs;

/// <summary>
/// Represents the data required to create a new product.
/// </summary>
public class CreateProductDto
{
    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the initial quantity of the product.
    /// </summary>
    public int Quantity { get; set; }
}