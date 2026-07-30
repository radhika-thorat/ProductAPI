using Microsoft.EntityFrameworkCore;
using ProductSolution.ProductDomain.Entities;

/// <summary>
/// Represents the application's database context.
/// Provides access to Product and Item entities.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">
    /// The options to configure the database context.
    /// </param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets the Product table.
    /// </summary>
    public DbSet<Product> Products => Set<Product>();

    /// <summary>
    /// Gets the Item table.
    /// </summary>
    public DbSet<Item> Items => Set<Item>();

    /// <summary>
    /// Configures the entity mappings and model relationships.
    /// </summary>
    /// <param name="modelBuilder">
    /// The model builder used to configure entity mappings.
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Map entities to database tables
        modelBuilder.Entity<Product>().ToTable("Product");
        modelBuilder.Entity<Item>().ToTable("Item");

        // Apply all IEntityTypeConfiguration implementations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}