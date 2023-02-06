using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyCore.Common.Base
{
    public class BaseModel
    {
        public int? ID { get; set; }
        public int ActivationStatus { get; set; }

        [NotMapped]
        [JsonIgnore]
        public ActivationStatusEnum ActivationStatusData { get; set; }
    }
    public class ExtendBaseModel : BaseModel
    {
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }
    }
}
