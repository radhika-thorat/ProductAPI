using AutoMapper;
using ProductSolution.ProductApplication.DTOs;
using ProductSolution.ProductDomain.Entities;

/// <summary>
/// Defines AutoMapper mappings between domain entities and DTOs.
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfile"/> class
    /// and configures object-to-object mappings.
    /// </summary>
    public MappingProfile()
    {
        // Maps Product entity to ProductDto
        CreateMap<Product, ProductDto>();

        // Maps CreateProductDto to Product entity
        CreateMap<CreateProductDto, Product>();

        // Maps UpdateProductDto to Product entity
        CreateMap<UpdateProductDto, Product>();
    }
}