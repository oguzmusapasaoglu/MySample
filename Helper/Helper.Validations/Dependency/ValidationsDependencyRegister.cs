using Helper.Validations.Interfaces;
using Helper.Validations.RoleValidator;
using Helper.Validations.UserValidator;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;
using Helper.Validations.Helper;

namespace Helper.Validations.Dependency;

public static class ValidationsDependencyRegister
{
    public static void ConfigureValidationsDependency(this IServiceCollection services)
    {
        services.AddScoped<IValidateManager, ValidateManager>();

        #region Users
        services.AddValidatorsFromAssemblyContaining<UserInfoValidator>();
        services.AddValidatorsFromAssemblyContaining<UserGroupValidator>();
        services.AddValidatorsFromAssemblyContaining<UsersRolesValidator>();
        #endregion

        #region Roles
        services.AddValidatorsFromAssemblyContaining<RolesValidator>();
        services.AddValidatorsFromAssemblyContaining<RolePageObjectValidator>();
        services.AddValidatorsFromAssemblyContaining<PageValidator>();
        services.AddValidatorsFromAssemblyContaining<PageObjectValidator>();
        #endregion
    }
}
