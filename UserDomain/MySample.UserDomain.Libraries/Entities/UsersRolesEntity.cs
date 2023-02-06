using MyCore.Dapper.Attributes;
using MyCore.Dapper.Base;

namespace MySample.UserDomain.Libraries.Entities;

[DapperTable("UsersRoles")]
public class UsersRolesEntity : ExtendBaseDapperEntity
{
    public int UserID { get; set; }
    public int RoleID { get; set; }
}