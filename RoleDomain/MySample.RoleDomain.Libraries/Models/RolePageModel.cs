using MyCore.Common.Base;

namespace MySample.RoleDomain.Libraries.Models
{
    public class RolePageModel : BaseModel
    {
        public int PageID { get; set; }
        public int RoleID { get; set; }
    }
    public class RolePageListModel : BaseModel
    {
        public int PageID { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string PageName { get; set; }
    }
}
