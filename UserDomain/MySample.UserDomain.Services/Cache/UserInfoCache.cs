using MySample.UserDomain.Libraries.Entities;
using MySample.UserDomain.Libraries.Models;
using MySample.UserDomain.Repositories.CacheInterfaces;
using MyCore.Cache.Interfaces;

using MySample.UserDomain.Data.Interfaces;

using Helper.Maps;
using System.Linq.Expressions;
namespace MySample.UserDomain.Repositories.Cache;
public class UserInfoCache : IUserInfoCache
{
    #region Fields
    public string CacheKey => cache.GetCacheKey(BaseName);
    public string BaseName => "UserInfo";
    IUserInfoRepository repository;
    IMemCacheServices cache;
    #endregion

    #region Ctor
    public UserInfoCache(IMemCacheServices _cache, IUserInfoRepository _repository)
    {
        cache = _cache;
        repository = _repository;
    }
    #endregion

    #region Methods
    public void AddBulkData(IQueryable<UserInfoModel> data)
    {
        var result = cache.GetCachedData<UserInfoModel>(CacheKey).ToList();
        result.AddRange(data);
        cache.FillCache(CacheKey, result);
    }
    public void AddSingleData(UserInfoModel data)
    {
        var result = GetAllData();
        result.ToList().Add(data);
        cache.FillCache(CacheKey, result);
    }
    public IQueryable<UserInfoModel> FillData()
    {
        var data = repository.GetAllActive();
        var model = MapperInstance.Instance.Map<IEnumerable<UserInfoEntity>, IEnumerable<UserInfoModel>>(data.Result);
        cache.FillCache(CacheKey, model);
        return model.AsQueryable();
    }
    public IQueryable<UserInfoModel> GetAllData()
    {
        var result = cache.GetCachedData<UserInfoModel>(CacheKey);
        if (result == null)
            result = FillData();
        return result;
    }
    public IQueryable<UserInfoModel> GetDataByFilter(Expression<Func<UserInfoModel, bool>> predicate)
    {
        var result = cache.GetCachedData<UserInfoModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.Where(predicate);
    }
    public UserInfoModel GetSingleDataByFilter(Expression<Func<UserInfoModel, bool>> predicate)
    {
        var result = cache.GetCachedData<UserInfoModel>(CacheKey);
        if (result.IsNullOrEmpty())
            result = FillData();
        return result.FirstOrDefault(predicate);
    }
    public UserInfoModel GetSingleDataById(int id)
    {
        var result = cache.GetCachedData<UserInfoModel>(CacheKey);
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
