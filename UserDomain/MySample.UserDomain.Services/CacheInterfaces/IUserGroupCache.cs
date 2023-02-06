using MyCore.Cache.Interfaces;
using MySample.UserDomain.Libraries.Models;

namespace MySample.UserDomain.Repositories.CacheInterfaces;

public interface IUserGroupCache : ICacheManager<UserGroupModel>
{
}