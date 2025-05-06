using Template.Application.DTOs.Request;
using FluentValidation;

namespace Template.Application.Validators;

public class ProductDTORequestValidator : AbstractValidator<ProductDtoRequest>
{
    public ProductDTORequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
    }
}