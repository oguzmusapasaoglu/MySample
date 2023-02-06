namespace MySample.RoleDomain.Libraries.Models;
public class LeftMainMenuModel
{
    public int ID { get; set; }
    public string PageName { get; set; }
    public string IconName { get; set; }
    public string PageURL { get; set; }
    public List<LeftSubMenuModel> LeftSubMenuList { get; set; }
}

public class LeftSubMenuModel
{
    public int ID { get; set; }
    public int BindPageID { get; set; }
    public string PageName { get; set; }
    public string IconName { get; set; }
    public string PageURL { get; set; }
}
