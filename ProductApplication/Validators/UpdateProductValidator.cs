using FluentValidation;
using ProductSolution.ProductApplication.DTOs;

/// <summary>
/// Validates the data required to update an existing product.
/// </summary>
public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProductValidator"/> class
    /// and defines validation rules for <see cref="UpdateProductDto"/>.
    /// </summary>
    public UpdateProductValidator()
    {
        // Validate Product Name
        RuleFor(x => x.ProductName)
            .NotEmpty()
            .MaximumLength(255);
    }
}