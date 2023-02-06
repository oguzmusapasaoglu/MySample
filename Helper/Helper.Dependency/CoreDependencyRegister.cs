using MyCore.Cache.Interfaces;
using MyCore.Dapper.Interfaces;
using MyCore.Dapper.Factory;
using MyCore.LogManager.Services;
using MyCore.Common.Services;
using Microsoft.Extensions.DependencyInjection;
using MyCore.Elastic.Interfaces;
using MyCore.Elastic.Services;

namespace Helper.Dependency;

public static class CoreDependencyRegister
{
    public static void ConfigureCoreDependency(this IServiceCollection services)
    {
        services.AddScoped<IMemCacheServices, MemCacheServices>();
        services.AddScoped<IConnectionFactory, ConnectionFactory>();
        services.AddScoped<IDbFactory, DbFactory>();
        services.AddScoped<ILogServices, LogServices>();
        services.AddScoped<IElasticManageServices, ElasticManageServices>();
    }
}
