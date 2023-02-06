using Helper.Maps;

using MyCore.Cache.Interfaces;

using MySample.UserDomain.Data.Interfaces;
using MySample.UserDomain.Libraries.Entities;
using MySample.UserDomain.Libraries.Models;
using MySample.UserDomain.Repositories.CacheInterfaces;

using System.Linq.Expressions;
namespace MySample.UserDomain.Repositories.Cache;
public class UsersRolesCache : IUsersRolesCache
{
    #region Fields
    public string CacheKey => cache.GetCacheKey(BaseName);
    public string BaseName => "UsersRoles";
    private IUsersRolesRepository repository;
    private IMemCacheServices cache;
    #endregion

    #region Ctor
    public UsersRolesCache(IMemCacheServices _cache, IUsersRolesRepository _repository)
    {
        cache = _cache;
        repository = _repository;
    }
    #endregion

    #region Methods
    public void AddBulkData(IQueryable<UsersRolesModel> data)
    {
        var result = cache.GetCachedData<UsersRolesModel>(CacheKey).ToList();
        result.AddRange(data);
        cache.FillCache(CacheKey, result);
    }
    public void AddSingleData(UsersRolesModel data)
    {
        var result = GetAllData();
        result.ToList().Add(data);
        cache.FillCache(CacheKey, result);
    }
    public IQueryable<UsersRolesModel> FillData()
    {
        var data = repository.GetAllActive();
        var model = MapperInstance.Instance.Map<IEnumerable<UsersRolesEntity>, IEnumerable<UsersRolesModel>>(data.Result);
        cache.FillCache(CacheKey, model);
        return model.AsQueryable();
    }
    public IQueryable<UsersRolesModel> GetAllData()
    {
        var result = cache.GetCachedData<UsersRolesModel>(CacheKey);
        if (result == null)
            result = FillData();
        return result;
    }
    public IQueryable<UsersRolesModel> GetDataByFilter(Expression<Func<UsersRolesModel, bool>> predicate)
    {
        var result = cache.GetCachedData<UsersRolesModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.Where(predicate);
    }
    public UsersRolesModel GetSingleDataByFilter(Expression<Func<UsersRolesModel, bool>> predicate)
    {
        var result = cache.GetCachedData<UsersRolesModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.FirstOrDefault(predicate);
    }
    public UsersRolesModel GetSingleDataById(int id)
    {
        var result = cache.GetCachedData<UsersRolesModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.FirstOrDefault(q => q.ID == id);
    }
    public void ReFillCache()
    {
        FillData();
    }
    public IQueryable<UsersRolesModel> GetAllDataByUserID(int userID)
    {
        var result = cache.GetCachedData<UsersRolesModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.Where(q => q.UserID == userID);
    }
    public IQueryable<UsersRolesModel> GetAllDataByRoleID(int roleID)
    {
        var result = cache.GetCachedData<UsersRolesModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.Where(q => q.RoleID == roleID);
    }
    #endregion
}
