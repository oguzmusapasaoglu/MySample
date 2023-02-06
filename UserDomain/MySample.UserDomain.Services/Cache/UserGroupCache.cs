using MySample.UserDomain.Libraries.Entities;
using MySample.UserDomain.Libraries.Models;
using MySample.UserDomain.Repositories.CacheInterfaces;
using MyCore.Cache.Interfaces;

using MySample.UserDomain.Data.Interfaces;

using System.Linq.Expressions;
using Helper.Maps;

namespace MySample.UserDomain.Repositories.Cache;
public class UserGroupCache : IUserGroupCache
{
    #region Fields
    public string CacheKey => cache.GetCacheKey(BaseName);
    public string BaseName => "UserGroup";
    IUserInfoRepository repository;
    IMemCacheServices cache;
    #endregion

    #region Ctor
    public UserGroupCache(IMemCacheServices _cache, IUserInfoRepository _repository)
    {
        cache = _cache;
        repository = _repository;
    }
    #endregion

    #region Methods
    public void AddBulkData(IQueryable<UserGroupModel> data)
    {
        var result = cache.GetCachedData<UserGroupModel>(CacheKey).ToList();
        result.AddRange(data);
        cache.FillCache(CacheKey, result);
    }
    public void AddSingleData(UserGroupModel data)
    {
        var result = GetAllData();
        result.ToList().Add(data);
        cache.FillCache(CacheKey, result);
    }
    public IQueryable<UserGroupModel> FillData()
    {
        var data = repository.GetAllActive();
        var model = MapperInstance.Instance.Map<IEnumerable<UserInfoEntity>, IEnumerable<UserGroupModel>>(data.Result);
        cache.FillCache(CacheKey, model);
        return model.AsQueryable();
    }
    public IQueryable<UserGroupModel> GetAllData()
    {
        var result = cache.GetCachedData<UserGroupModel>(CacheKey);
        if (result == null)
            result = FillData();
        return result;
    }
    public IQueryable<UserGroupModel> GetDataByFilter(Expression<Func<UserGroupModel, bool>> predicate)
    {
        var result = cache.GetCachedData<UserGroupModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.Where(predicate);
    }
    public UserGroupModel GetSingleDataByFilter(Expression<Func<UserGroupModel, bool>> predicate)
    {
        var result = cache.GetCachedData<UserGroupModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.FirstOrDefault(predicate);
    }
    public UserGroupModel GetSingleDataById(int id)
    {
        var result = cache.GetCachedData<UserGroupModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.FirstOrDefault(q => q.ID == id);
    }
    public void ReFillCache()
    {
        FillData();
    }
    #endregion
}