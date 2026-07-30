namespace ProductSolution.Domain.Events;

/// <summary>
/// Represents a domain event that occurred within the domain model.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Gets the date and time when the domain event occurred.
    /// </summary>
    DateTime OccurredOn { get; }
}