using System.Linq.Expressions;

namespace MyCore.Cache.Interfaces
{
    public interface ICacheManager<TModel> where TModel : class
    {
        string BaseName { get; }
        string CacheKey { get; }
        IQueryable<TModel> GetAllData();
        IQueryable<TModel> GetDataByFilter(Expression<Func<TModel, bool>> predicate);
        TModel GetSingleDataById(int id);
        TModel GetSingleDataByFilter(Expression<Func<TModel, bool>> predicate);
        IQueryable<TModel> FillData();
        void AddBulkData(IQueryable<TModel> data);
        void AddSingleData(TModel data);
        void ReFillCache(); 
    }
}
