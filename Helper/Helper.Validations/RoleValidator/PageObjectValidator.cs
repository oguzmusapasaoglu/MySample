using FluentValidation;

using MyCore.LogManager.ExceptionHandling;

using MySample.RoleDomain.Libraries.Entities;

namespace Helper.Validations.RoleValidator;

public class PageObjectValidator : AbstractValidator<PageObjectEntity>
{
    public PageObjectValidator()
    {
        RuleFor(x => x.PageID)
            .NotEmpty().WithMessage(ExceptionMessageHelper.RequiredField("Page"));

        RuleFor(x => x.PageObjectName)
            .NotEmpty().WithMessage(ExceptionMessageHelper.RequiredField("Page Object Name"))
            .MaximumLength(150).WithMessage(ExceptionMessageHelper.LengthError("Page Object Name", 150));

        RuleFor(x => x.ServicesName)
            .NotEmpty().WithMessage(ExceptionMessageHelper.RequiredField("Services Name"))
            .MaximumLength(150).WithMessage(ExceptionMessageHelper.LengthError("Services Name", 150));

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(ExceptionMessageHelper.RequiredField("Description"))
            .MaximumLength(500).WithMessage(ExceptionMessageHelper.LengthError("Description", 500));
    }
}
