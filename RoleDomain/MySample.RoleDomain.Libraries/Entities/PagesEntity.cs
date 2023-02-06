using MyCore.Dapper.Attributes;
using MyCore.Dapper.Base;

namespace MySample.RoleDomain.Libraries.Entities;

[DapperTable("Pages")]

public class PagesEntity : ExtendBaseDapperEntity
{
    public int BindPageId { get; set; }
    public string IconName { get; set; }
    public string PageName { get; set; }
    public string Description { get; set; }
    public string PageURL { get; set; }
    public int PageLevel { get; set; }

    [DapperWrite(false)]
    public ICollection<PageObjectEntity> PageObjects { get; set; }
}