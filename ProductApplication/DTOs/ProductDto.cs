using System.ComponentModel.DataAnnotations.Schema;

namespace ProductSolution.ProductApplication.DTOs;

/// <summary>
/// Represents product information returned by the API.
/// </summary>
public class ProductDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity of the product.
    /// This property is not mapped to the Product database table.
    /// </summary>
    [NotMapped]
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the name of the user who created the product.
    /// </summary>
    public string CreatedBy { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date and time when the product was created.
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Gets or sets the name of the user who last modified the product.
    /// </summary>
    public string? ModifiedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the product was last modified.
    /// </summary>
    public DateTime? ModifiedOn { get; set; }
}