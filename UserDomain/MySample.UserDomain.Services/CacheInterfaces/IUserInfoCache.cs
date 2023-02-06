using MyCore.Cache.Interfaces;
using MySample.UserDomain.Libraries.Models;

namespace MySample.UserDomain.Repositories.CacheInterfaces;
public interface IUserInfoCache : ICacheManager<UserInfoModel>
{
}