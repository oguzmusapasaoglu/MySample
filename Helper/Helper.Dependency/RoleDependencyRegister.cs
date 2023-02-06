using Microsoft.Extensions.DependencyInjection;

using MySample.RoleDomain.Repositores.Interfaces;
using MySample.RoleDomain.Repositores.RepositoryManager;
using MySample.RoleDomain.Services.Cache;
using MySample.RoleDomain.Services.CacheInterfaces;
using MySample.RoleDomain.Services.Interfaces;
using MySample.RoleDomain.Services.ServicesManager;

namespace Helper.Dependency;
public static class RoleDependencyRegister
{
    public static void ConfigureRoleDependency(this IServiceCollection services)
    {
        services.AddScoped<IRolesServices, RolesServices>();
        services.AddScoped<IRolesCache, RolesCache>();
        services.AddScoped<IRolesRepository, RolesRepository>();

        services.AddScoped<IRolePageObjectServices, RolePageObjectServices>();
        services.AddScoped<IRolePageObjectCache, RolePageObjectCache>();
        services.AddScoped<IRolePageObjectRepository, RolePageObjectRepository>();

        services.AddScoped<IPagesServices, PagesServices>();
        services.AddScoped<IPagesCache, PagesCache>();
        services.AddScoped<IPagesRepository, PagesRepository>();

        services.AddScoped<IPageObjectServices, PageObjectServices>();
        services.AddScoped<IPageObjectCache, PageObjectCache>();
        services.AddScoped<IPageObjectRepository, PageObjectRepository>();

        services.AddScoped<IAuthorizationControlCache, AuthorizationControlCache>();
        services.AddScoped<IAuthorizationControlServices, AuthorizationControlServices>();
    }
}
