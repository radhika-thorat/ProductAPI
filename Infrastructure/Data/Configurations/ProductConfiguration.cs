using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductSolution.ProductDomain.Entities;

/// <summary>
/// Configures the database mapping for the <see cref="Product"/> entity.
/// </summary>
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    /// <summary>
    /// Configures the Product entity properties and constraints.
    /// </summary>
    /// <param name="builder">
    /// The entity type builder used to configure the Product entity.
    /// </param>
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Map entity to Product table
        builder.ToTable("Product");

        // Configure primary key
        builder.HasKey(x => x.Id);

        // Configure ProductName column
        builder.Property(x => x.ProductName)
               .HasMaxLength(255)
               .IsRequired();

        // Configure CreatedBy column
        builder.Property(x => x.CreatedBy)
               .HasMaxLength(100)
               .IsRequired();

        // Configure ModifiedBy column
        builder.Property(x => x.ModifiedBy)
               .HasMaxLength(100);
    }
}