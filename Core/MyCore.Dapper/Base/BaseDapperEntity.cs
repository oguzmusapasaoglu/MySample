using Dapper.Contrib.Extensions;

namespace MyCore.Dapper.Base
{
    public class BaseDapperEntity
    {
        [Key]
        public int ID { get; set; }
        public int ActivationStatus { get; set; }
    }

    public class ExtendBaseDapperEntity : BaseDapperEntity
    {
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }
    }
}
