namespace MySample.RoleDomain.Libraries.Models
{
    public class AuthorizationControlModel
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public int PageObjectID { get; set; }
        public string PageObjectName { get; set; }
        public string ServicesName { get; set; }
    }
}
