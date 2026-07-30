using ProductSolution.ProductDomain.Entities;

namespace ProductSolution.Domain.Events;

/// <summary>
/// Represents a domain event that is raised when a new product is created.
/// </summary>
public class ProductCreatedEvent : IDomainEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductCreatedEvent"/> class.
    /// </summary>
    /// <param name="product">The product that was created.</param>
    public ProductCreatedEvent(Product product)
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