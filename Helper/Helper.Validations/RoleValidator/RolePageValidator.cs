using FluentValidation;

using MyCore.LogManager.ExceptionHandling;

using MySample.RoleDomain.Libraries.Entities;

namespace Helper.Validations.RoleValidator
{
    public class RolePageValidator : AbstractValidator<RolePageEntity>
    {
        public RolePageValidator()
        {
            RuleFor(x => x.RoleID)
              .NotEmpty().WithMessage(ExceptionMessageHelper.RequiredField("Role"));

            RuleFor(x => x.PageID)
              .NotEmpty().WithMessage(ExceptionMessageHelper.RequiredField("Page"));
        }
    }
}
