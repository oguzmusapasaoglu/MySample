using MyCore.Common.Base;

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace MySample.RoleDomain.Libraries.Models;

public class PageObjectModel : ExtendBaseModel
{
    public int PageID { get; set; }
    public string PageObjectName { get; set; }
    
    [JsonIgnore]
    public string ServicesName { get; set; }
    public string Description { get; set; }

    [NotMapped]
    public PagesModel Page { get; set; }
    public string ActivationStatusName { get; set; }
}
public class PageObjectCreateOrUpdateModel : BaseModel
{
    public int PageID { get; set; }
    public string PageObjectName { get; set; }
    public string Description { get; set; }
}
public class PageObjectFilterModel
{
    public int? Id { get; set; }
    public int? ActivationStatus { get; set; }
    public string PageObjectName { get; set; }
}
public class PageObjectStatusChangeModel
{
    public int ID { get; set; }
    public int ActivationStatus { get; set; }

    [JsonIgnore]
    [NotMapped]
    public ActivationStatusEnum ActivationStatusValue { get; set; }
}