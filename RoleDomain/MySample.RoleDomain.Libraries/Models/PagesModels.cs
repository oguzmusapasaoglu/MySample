using MyCore.Common.Base;

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MySample.RoleDomain.Libraries.Models
{
    public class PagesModel : ExtendBaseModel
    {
        public int BindPageId { get; set; }
        public string IconName { get; set; }
        public string PageName { get; set; }
        public string Description { get; set; }
        public string PageURL { get; set; }
        public int PageLevel { get; set; }
        public string ActivationStatusName { get; set; }

        [NotMapped]
        public IQueryable<PageObjectModel> PagesObjects { get; set; }
    }
    public class PagesCreateOrUpdateModel : BaseModel
    {
        public string PagesName { get; set; }
        public string Description { get; set; }
    }
    public class PagesFilterModel
    {
        public int? Id { get; set; }
        public int? ActivationStatus { get; set; }
        public string PageName { get; set; }
    }
    public class PagesStatusChangeModel
    {
        public int ID { get; set; }
        public int ActivationStatus { get; set; }

        [JsonIgnore]
        [NotMapped]
        public ActivationStatusEnum ActivationStatusValue { get; set; }
    }
}