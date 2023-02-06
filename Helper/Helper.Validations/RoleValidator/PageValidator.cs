using FluentValidation;

using MyCore.LogManager.ExceptionHandling;

using MySample.RoleDomain.Libraries.Entities;

namespace Helper.Validations.RoleValidator;

public class PageValidator : AbstractValidator<PagesEntity>
{
    public PageValidator()
    {
        RuleFor(x => x.PageName)
            .NotEmpty().WithMessage(ExceptionMessageHelper.RequiredField("Page Name"))
            .MaximumLength(150).WithMessage(ExceptionMessageHelper.LengthError("Page Name", 150));

        RuleFor(x => x.PageName)
            .NotEmpty().WithMessage(ExceptionMessageHelper.RequiredField("Description"))
            .MaximumLength(500).WithMessage(ExceptionMessageHelper.LengthError("Description", 500));

        RuleFor(x => x.PageLevel)
            .NotEmpty().WithMessage(ExceptionMessageHelper.RequiredField("Page Level"))
            .GreaterThan(1).WithMessage(ExceptionMessageHelper.RequiredField("Page Name"));

    }
}
