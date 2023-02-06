using MyCore.Dapper.Attributes;
using MyCore.Dapper.Base;

namespace MySample.RoleDomain.Libraries.Entities
{
    [DapperTable("PageObject")]
    public class PageObjectEntity : ExtendBaseDapperEntity
    {
        public int PageID { get; set; }
        public string PageObjectName { get; set; }
        public string Description { get; set; }
        public string ServicesName { get; set; }

        [DapperWrite(false)]
        public PagesEntity Page { get; set; }
    }
}
