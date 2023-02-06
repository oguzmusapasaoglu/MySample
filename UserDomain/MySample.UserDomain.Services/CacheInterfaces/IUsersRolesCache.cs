using MyCore.Cache.Interfaces;
using MySample.UserDomain.Libraries.Models;

namespace MySample.UserDomain.Repositories.CacheInterfaces;
public interface IUsersRolesCache : ICacheManager<UsersRolesModel>
{
    IQueryable<UsersRolesModel> GetAllDataByUserID(int userID);
    IQueryable<UsersRolesModel> GetAllDataByRoleID(int roleID);
}
