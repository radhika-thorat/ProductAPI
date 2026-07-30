namespace ProductSolution.ProductDomain.Entities
{
    /// <summary>
    /// Represents the base entity for all domain entities.
    /// Provides a common primary key property.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the entity.
        /// </summary>
        public int Id { get; set; }
    }
}