using MyCore.Dapper.Attributes;
using MyCore.Dapper.Base;

namespace MySample.RoleDomain.Libraries.Entities
{
    public class RolePageEntity : BaseDapperEntity
    {
        public int PageID { get; set; }
        public int RoleID { get; set; }

        [DapperWrite(false)]
        public string RoleName { get; set; }

        [DapperWrite(false)]
        public string PageName { get; set; }
    }
}
