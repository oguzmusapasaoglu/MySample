using MyCore.Cache.Interfaces;
using MySample.RoleDomain.Libraries.Models;

namespace MySample.RoleDomain.Services.CacheInterfaces;

public interface IRolePageObjectCache : ICacheManager<RolePageObjectModel>
{
    IQueryable<RolePageObjectModel> GetDataByRoleId(int roleId);
}