using MyCore.Cache.Interfaces;
using MyCore.Common.ConfigHelper;
using MyCore.Dapper.Interfaces;

using MySample.RoleDomain.Libraries.Models;
using MySample.RoleDomain.Services.CacheInterfaces;

using System.Text;
namespace MySample.RoleDomain.Services.Cache;
public class AuthorizationControlCache : IAuthorizationControlCache
{
    #region Fields
    private string connectionString => MySampleSettingsConfigModelHelper.GetConnection();
    public string CacheKey => cache.GetCacheKey(BaseName);
    public string BaseName => "AuthorizationControl";
    IMemCacheServices cache;
    IDbFactory dbFactory;
    #endregion

    #region Ctor
    public AuthorizationControlCache(IMemCacheServices _cache, IDbFactory _dbFactory)
    {
        cache = _cache;
        dbFactory = _dbFactory;
    }
    #endregion

    #region Methods
    public void AddBulkData(IQueryable<AuthorizationControlModel> data)
    {
        var result = cache.GetCachedData<AuthorizationControlModel>(CacheKey).ToList();
        result.AddRange(data);
        cache.FillCache(CacheKey, result);
    }
    public void AddSingleData(AuthorizationControlModel data)
    {
        var result = GetAllData();
        result.ToList().Add(data);
        cache.FillCache(CacheKey, result);
    }
    public IQueryable<AuthorizationControlModel> FillAllData()
    {
        StringBuilder sbQuery = new StringBuilder();
        sbQuery.AppendLine(@"SELECT ")
            .AppendLine(" UR.UserID, RPO.RoleID, RPO.PageObjectID, PO.PageObjectName, PO.ServicesName ")
            .AppendLine(" FROM PageObjects PO INNER JOIN ")
            .AppendLine(" RolePageObject RPO ON PO.ID = RPO.PageObjectID INNER JOIN ")
            .AppendLine(" UsersRoles UR ON RPO.RoleID = UR.RoleID ")
            .AppendLine(" WHERE (PO.ActivationStatus = 1 AND RPO.ActivationStatus = 1 AND UR.ActivationStatus = 1)");
        var model = dbFactory.GetData<AuthorizationControlModel>(connectionString, sbQuery);
        cache.FillCache(CacheKey, model.Result);
        return model.Result.AsQueryable();
    }
    public IQueryable<AuthorizationControlModel> GetAllData()
    {
        var result = cache.GetCachedData<AuthorizationControlModel>(CacheKey);
        if (result == null)
            result = FillAllData();
        return result;
    }
    public IQueryable<AuthorizationControlModel> GetDataByUserID(int userID)
    {
        var data = cache.GetCachedData<AuthorizationControlModel>(CacheKey);
        return data.Where(q => q.UserID == userID);
    }
    public void ReFillCache()
    {
        FillAllData();
    }
    #endregion
}