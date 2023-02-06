using MyCore.Dapper.Attributes;
using MyCore.Dapper.Base;
namespace MySample.UserDomain.Libraries.Entities;

[DapperTable("UserInfo")]
public class UserInfoEntity : ExtendBaseDapperEntity
{
    public string NameSurname { get; set; }
    public string UserName { get; set; }
    public string EMail { get; set; }
    public string Password { get; set; }
    public string GSM { get; set; }
    public int UserGroupID { get; set; }
    public string UserType { get; set; }
    public DateTime? BirthDay { get; set; }

    [DapperWrite(false)]
    public UserGroupEntity UserGroup { get; set; }
}
