using FluentValidation;
using Template.Application.DTOs.Request;

namespace Template.Application.Validators;

public class ProductDTORequestValidator : AbstractValidator<ProductDtoRequest>
{
    public ProductDTORequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
    }
}
