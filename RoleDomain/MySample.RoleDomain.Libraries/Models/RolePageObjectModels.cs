using MyCore.Common.Base;

using System.ComponentModel.DataAnnotations.Schema;

namespace MySample.RoleDomain.Libraries.Models;
public class RolePageObjectModel : ExtendBaseModel
{
    public int RoleID { get; set; }
    public int PageObjectID { get; set; }
    public string ActivationStatusName { get; set; }

    [NotMapped]
    public RolesModel Role { get; set; }

    [NotMapped]
    public PageObjectModel PageObject { get; set; }
}
public class RolePageObjectCreateOrUpdateModel : BaseModel
{
    public int RoleID { get; set; }
    public int PageObjectID { get; set; }
}
public class RolePageObjectFilterModel
{
    public int? ActivationStatus { get; set; }
    public int RoleID { get; set; }
    public int PageObjectID { get; set; }
}