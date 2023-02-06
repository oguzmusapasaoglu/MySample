using MyCore.Cache.Interfaces;
using MySample.RoleDomain.Libraries.Models;

namespace MySample.RoleDomain.Services.CacheInterfaces;

public interface IRolePageCache : ICacheManager<RolePageListModel>
{
}
