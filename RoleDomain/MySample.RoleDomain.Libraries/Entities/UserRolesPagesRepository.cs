namespace MySample.RoleDomain.Libraries.Entities
{
    public class UserRolesPagesRepository
    {
        public int ID { get; set; }
        public int BindPageId { get; set; }
        public string PageName { get; set; }
        public string IconName { get; set; }
        public string PageURL { get; set; }
        public int PageLevel { get; set; }
        public string Description { get; set; }
        public string RoleName { get; set; }
        public int RoleID { get; set; }
    }
}
