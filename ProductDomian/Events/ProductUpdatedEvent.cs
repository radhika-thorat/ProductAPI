using ProductSolution.ProductDomain.Entities;

namespace ProductSolution.Domain.Events;

/// <summary>
/// Represents a domain event that is raised when an existing product is updated.
/// </summary>
public class ProductUpdatedEvent : IDomainEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductUpdatedEvent"/> class.
    /// </summary>
    /// <param name="product">The product that was updated.</param>
    public ProductUpdatedEvent(Product product)
    {
        Product = product;
        OccurredOn = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets the product associated with the event.
    /// </summary>
    public Product Product { get; }

    /// <summary>
    /// Gets the date and time when the event occurred.
    /// </summary>
    public DateTime OccurredOn { get; }
}