using FluentValidation;
using ProductApplication.DTOs;


namespace ProductSolution.Application.Validators;

public class UpdateItemValidator : AbstractValidator<UpdateItemDto>
{
    public UpdateItemValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}