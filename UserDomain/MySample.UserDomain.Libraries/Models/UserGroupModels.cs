using MyCore.Common.Base;
namespace MySample.UserDomain.Libraries.Models;
public class UserGroupModel : ExtendBaseModel
{
    public string GroupName { get; set; }
    public string Descriptions { get; set; }
    public string ActivationStatusName { get; set; }
}
public class UserGroupCreateOrUpdateModel : BaseModel
{
    public string GroupName { get; set; }
    public string Descriptions { get; set; }
}
public class UserGroupFilterModel
{
    public int? ActivationStatus { get; set; }
    public string GroupName { get; set; }
    public string Descriptions { get; set; }
}
