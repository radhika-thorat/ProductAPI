using FluentValidation;
using Services;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));

        services.AddValidatorsFromAssembly(typeof(MappingProfile).Assembly);

        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<IItemService, ItemService>();

        return services;
    }
}