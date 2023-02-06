using MySample.RoleDomain.Libraries.Entities;
using MySample.UserDomain.Libraries.Entities;

namespace Helper.Validations.Interfaces;

public interface IValidateManager
{
    #region Users
    Task<List<string>> UsersValidate(UserInfoEntity entity);
    Task<List<string>> UsersValidate(UserGroupEntity entity);
    Task<List<string>> UsersValidate(UsersRolesEntity entity);
    #endregion

    #region Roles
    Task<List<string>> RolesValidate(RolesEntity entity);
    Task<List<string>> RolesValidate(RolePageEntity entity);
    Task<List<string>> RolesValidate(RolePageObjectEntity entity);
    Task<List<string>> RolesValidate(PagesEntity entity);
    Task<List<string>> RolesValidate(PageObjectEntity entity);
    #endregion
}