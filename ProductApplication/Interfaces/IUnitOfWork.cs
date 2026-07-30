namespace ProductSolution.ProductApplication.Interfaces;

/// <summary>
/// Defines the Unit of Work contract for managing database transactions.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saves all changes made in the current unit of work to the database.
    /// </summary>
    /// <param name="cancellationToken">
    /// A token used to cancel the save operation.
    /// </param>
    /// <returns>
    /// The number of state entries written to the database.
    /// </returns>
    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}