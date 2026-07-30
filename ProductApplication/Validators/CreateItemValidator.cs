using FluentValidation;
using ProductApplication.DTOs;
using ProductSolution.Application.Validators;

public class CreateItemValidator : AbstractValidator<CreateItemDto>
{
    public CreateItemValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0);

        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}