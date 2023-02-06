using MySample.RoleDomain.Libraries.Models;

namespace MySample.RoleDomain.Services.CacheInterfaces;

public interface IAuthorizationControlCache
{
    string BaseName { get; }
    string CacheKey { get; }
    IQueryable<AuthorizationControlModel> GetAllData();
    IQueryable<AuthorizationControlModel> GetDataByUserID(int userID);
    IQueryable<AuthorizationControlModel> FillAllData();
    void AddBulkData(IQueryable<AuthorizationControlModel> data);
    void AddSingleData(AuthorizationControlModel data);
    void ReFillCache();
}
