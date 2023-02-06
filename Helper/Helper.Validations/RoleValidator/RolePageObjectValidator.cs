using FluentValidation;

using MyCore.LogManager.ExceptionHandling;

using MySample.RoleDomain.Libraries.Entities;

namespace Helper.Validations.RoleValidator;

public class RolePageObjectValidator : AbstractValidator<RolePageObjectEntity>
{
    public RolePageObjectValidator()
    {
        RuleFor(x => x.RoleID)
          .NotEmpty().WithMessage(ExceptionMessageHelper.RequiredField("Role"));

        RuleFor(x => x.PageObjectID)
          .NotEmpty().WithMessage(ExceptionMessageHelper.RequiredField("Page Object"));
    }
}
