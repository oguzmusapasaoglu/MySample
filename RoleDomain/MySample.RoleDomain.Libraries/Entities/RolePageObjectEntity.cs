using MyCore.Dapper.Attributes;
using MyCore.Dapper.Base;

namespace MySample.RoleDomain.Libraries.Entities;

[DapperTable("RolePageObject")]
public class RolePageObjectEntity : BaseDapperEntity
{
    public int RoleID { get; set; }
    public int PageObjectID { get; set; }

    [DapperWrite(false)]
    public RolesEntity Role { get; set; }

    [DapperWrite(false)]
    public PageObjectEntity PageObject { get; set; }

}
