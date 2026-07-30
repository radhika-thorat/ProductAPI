using FluentValidation;
using ProductSolution.ProductApplication.DTOs;

/// <summary>
/// Validates the data required to create a new product.
/// </summary>
public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductValidator"/> class
    /// and defines validation rules for <see cref="CreateProductDto"/>.
    /// </summary>
    public CreateProductValidator()
    {
        // Validate Product Name
        RuleFor(x => x.ProductName)
            .NotEmpty()
            .MaximumLength(255);
    }
}