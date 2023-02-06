using MyCore.Common.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace MySample.RoleDomain.Libraries.Models;

public class RolesModel : ExtendBaseModel
{
    public string RoleName { get; set; }
    public string Description { get; set; }
    public string ActivationStatusName { get; set; }

    [NotMapped]
    public IQueryable<RolePageObjectModel> RolePageObjects { get; set; }
}
public class RolesCreateOrUpdateModel : BaseModel
{
    public string RoleName { get; set; }
    public string Descriptions { get; set; }
}
public class RolesListModel : BaseModel
{
    public string RoleName { get; set; }
    public string Description { get; set; }
    public string ActivationStatusName { get; set; }
}
public class RolesFilterModel
{
    public int? Id { get; set; }
    public int? ActivationStatus { get; set; }
    public string RoleName { get; set; }
}
public class RolesStatusChangeModel
{
    public int ID { get; set; }
    public int ActivationStatus { get; set; }

    [JsonIgnore]
    [NotMapped]
    public ActivationStatusEnum ActivationStatusValue { get; set; }
}