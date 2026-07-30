namespace ProductSolution.ProductApplication.DTOs;

/// <summary>
/// Represents the data required to update an existing product.
/// </summary>
public class UpdateProductDto
{
    /// <summary>
    /// Gets or sets the updated name of the product.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the updated quantity of the product.
    /// </summary>
    public int Quantity { get; set; }
}