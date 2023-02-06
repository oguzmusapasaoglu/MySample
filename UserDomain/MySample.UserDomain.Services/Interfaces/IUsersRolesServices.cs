using MyCore.Common.Base;
using MySample.UserDomain.Libraries.Models;

namespace MySample.UserDomain.Services.Interfaces;
public interface IUsersRolesServices
{
    ResponseBase<UsersRolesModel> CreateOrUpdate(RequestBase<UsersRolesCreateOrUpdateModel> request);
    ResponseBase<UsersRolesModel> BulkCreate(RequestBase<List<UsersRolesCreateOrUpdateModel>> request);
    ResponseBase<IQueryable<UsersRolesModel>> GetDataByUserID(int userID);
    ResponseBase<IQueryable<UsersRolesModel>> GetDataByRoleID(int roleID);
}
