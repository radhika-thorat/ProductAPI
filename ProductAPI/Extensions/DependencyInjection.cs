using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProductSolution.Infrastructure.Data;
using ProductSolution.Infrastructure.Repositories;
using ProductSolution.ProductApplication.Interfaces;
using ProductSolution.Services;

namespace ProductSolution.ProductAPI.Extensions;

/// <summary>
/// Provides extension methods for registering application
/// and infrastructure services in the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers application layer services.
    /// </summary>
    /// <param name="services">
    /// The service collection used to register application services.
    /// </param>
    /// <returns>
    /// The updated <see cref="IServiceCollection"/> instance.
    /// </returns>
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // Register AutoMapper profiles.
        services.AddAutoMapper(typeof(MappingProfile));

        // Register FluentValidation validators.
        services.AddValidatorsFromAssembly(typeof(MappingProfile).Assembly);

        // Register application services.
        services.AddScoped<IProductService, ProductService>();

        return services;
    }

    /// <summary>
    /// Registers infrastructure layer services.
    /// </summary>
    /// <param name="services">
    /// The service collection used to register infrastructure services.
    /// </param>
    /// <param name="configuration">
    /// The application configuration used to retrieve the database connection string.
    /// </param>
    /// <returns>
    /// The updated <see cref="IServiceCollection"/> instance.
    /// </returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register Entity Framework Core DbContext.
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        // Register repository.
        services.AddScoped<IProductRepository, ProductRepository>();

        // Register Unit of Work.
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}