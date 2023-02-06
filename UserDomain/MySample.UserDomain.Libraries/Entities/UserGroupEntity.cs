using MyCore.Dapper.Attributes;
using MyCore.Dapper.Base;
namespace MySample.UserDomain.Libraries.Entities;

[DapperTable("UserGroup")]
public class UserGroupEntity : ExtendBaseDapperEntity
{
    public string GroupName { get; set; }
    public string Descriptions { get; set; }
}
