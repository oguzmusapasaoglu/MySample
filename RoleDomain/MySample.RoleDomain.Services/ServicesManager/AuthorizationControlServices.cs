using MySample.RoleDomain.Services.CacheInterfaces;
using MySample.RoleDomain.Services.Interfaces;

namespace MySample.RoleDomain.Services.ServicesManager;

public class AuthorizationControlServices : IAuthorizationControlServices
{
    private IAuthorizationControlCache cache;

    public AuthorizationControlServices(IAuthorizationControlCache _cache)
    {
        cache = _cache;
    }
    public bool AuthorizationControlByUser(int userID, string servicesName)
    {
        var userData = cache.GetDataByUserID(userID);
        if (userData.Any() && userData.Any(q => q.ServicesName == servicesName))
            return true;
        return false;
    }
}
