using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using ProductApplication.Interfaces;
using Services;

namespace ProductAPI.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));

        services.AddValidatorsFromAssembly(typeof(MappingProfile).Assembly);

        services.AddScoped<IProductService, ProductService>();        

        return services;
    }

    public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}