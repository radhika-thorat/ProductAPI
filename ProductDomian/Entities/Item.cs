using System.ComponentModel.DataAnnotations.Schema;

namespace ProductSolution.ProductDomain.Entities
{
    /// <summary>
    /// Represents the inventory details associated with a product.
    /// </summary>
    [Table("Item")]
    public class Item
    {
        /// <summary>
        /// Gets or sets the unique identifier of the item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the associated product.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the available quantity of the product.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the associated product.
        /// </summary>
        public Product Product { get; set; } = null!;
    }
}