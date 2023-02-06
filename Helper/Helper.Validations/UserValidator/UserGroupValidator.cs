using FluentValidation;

using MyCore.LogManager.ExceptionHandling;

using MySample.UserDomain.Libraries.Entities;

namespace Helper.Validations.UserValidator;

public class UserGroupValidator : AbstractValidator<UserGroupEntity>
{
    public UserGroupValidator()
    {
        RuleFor(x => x.GroupName)
            .NotEmpty().WithMessage(ExceptionMessageHelper.RequiredField("Group Name"))
            .MaximumLength(150).WithMessage(ExceptionMessageHelper.LengthError("Group Name", 150));

        RuleFor(x => x.Descriptions)
            .NotEmpty().WithMessage(ExceptionMessageHelper.RequiredField("Descriptions"))
            .MaximumLength(500).WithMessage(ExceptionMessageHelper.LengthError("Descriptions", 500));
    }
}
