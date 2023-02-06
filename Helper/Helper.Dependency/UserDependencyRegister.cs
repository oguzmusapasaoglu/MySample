using Microsoft.Extensions.DependencyInjection;

using MySample.UserDomain.Data.Interfaces;
using MySample.UserDomain.Repositories.Cache;
using MySample.UserDomain.Repositories.CacheInterfaces;
using MySample.UserDomain.Repositories.RepositoryManager;
using MySample.UserDomain.Services.Interfaces;
using MySample.UserDomain.Services.ServicesManager;

namespace Helper.Dependency;
public static class UserDependencyRegister
{
    public static void ConfigureUserDependency(this IServiceCollection services)
    {
        services.AddScoped<IUserInfoRepository, UserInfoRepository>();
        services.AddScoped<IUserInfoCache, UserInfoCache>();
        services.AddScoped<IUserInfoServices, UserInfoServices>();

        services.AddScoped<IUserGroupServices, UserGroupServices>();
        services.AddScoped<IUserGroupCache, UserGroupCache>();
        services.AddScoped<IUserGroupRepository, UserGroupRepository>();

        services.AddScoped<IUsersRolesServices, UsersRolesServices>();
        services.AddScoped<IUsersRolesCache, UsersRolesCache>();
        services.AddScoped<IUsersRolesRepository, UsersRolesRepository>();
    }
}