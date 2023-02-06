using MyCore.Dapper.Base;

using Dapper;

using System.Linq.Expressions;
using System.Text;

namespace MyCore.Dapper.Interfaces
{
    public interface IDbFactory
    {
        Task<int?> InsertEntity<TEntity>(string connectionString, TEntity entity)
        where TEntity : BaseDapperEntity;
        Task<bool> UpdateEntity<TEntity>(string connectionString, TEntity entity)
            where TEntity : BaseDapperEntity;
        Task<bool> InsertBulkEntity<TEntity>(string connectionString, List<TEntity> entities)
            where TEntity : BaseDapperEntity;
        Task<TEntity> GetSingleById<TEntity>(string connectionString, int id)
         where TEntity : BaseDapperEntity;
        Task<IQueryable<TEntity>> GetAll<TEntity>(string connectionString)
            where TEntity : BaseDapperEntity;
        Task<IQueryable<TEntity>> GetAllByFilter<TEntity>(string connectionString, Expression<Func<TEntity, bool>> filter)
            where TEntity : BaseDapperEntity;
        Task<IQueryable<TEntity>> GetData<TEntity>(string connectionString, StringBuilder queryScript, DynamicParameters parameters);
        Task<IQueryable<TEntity>> GetData<TEntity>(string connectionString, StringBuilder queryScript);
        Task<TEntity> GetSingle<TEntity>(string connectionString, StringBuilder queryScript);
    }
}