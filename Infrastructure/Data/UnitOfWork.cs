namespace ProductSolution.Infrastructure.Data;

using ProductSolution.ProductApplication.Interfaces;

/// <summary>
/// Represents the Unit of Work pattern for managing database transactions.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="context">
    /// The application database context.
    /// </param>
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Saves all changes made in the current unit of work to the database.
    /// </summary>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// The number of state entries written to the database.
    /// </returns>
    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}