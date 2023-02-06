namespace MySample.RoleDomain.Services.Interfaces;

public interface IAuthorizationControlServices
{
    bool AuthorizationControlByUser(int userID, string servicesName);
}
