namespace ProductSolution.ProductDomain.Entities
{
    /// <summary>
    /// Represents a product in the system.
    /// Inherits common properties from <see cref="BaseEntity"/>.
    /// </summary>
    public class Product : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

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
}