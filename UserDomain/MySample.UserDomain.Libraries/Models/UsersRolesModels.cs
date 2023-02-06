using MyCore.Common.Base;
namespace MySample.UserDomain.Libraries.Models;
public class UsersRolesModel : ExtendBaseModel
{
    public int UserID { get; set; }
    public int RoleID { get; set; }
    public string UserName { get; set; }
    public string RoleName { get; set; }
    public string ActivationStatusName { get; set; }
}
public class UsersRolesCreateOrUpdateModel : BaseModel
{
    public int UserID { get; set; }
    public int RoleID { get; set; }
}