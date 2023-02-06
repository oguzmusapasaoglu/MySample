using MyCore.Dapper.Attributes;
using MyCore.Dapper.Base;

namespace MySample.RoleDomain.Libraries.Entities;

[DapperTable("Roles")]
public class RolesEntity : ExtendBaseDapperEntity
{
    public string RoleName { get; set; }
    public string Description { get; set; }

    [DapperWrite(false)]
    public ICollection<RolePageObjectEntity> RolePageObjects { get; set; }
}
