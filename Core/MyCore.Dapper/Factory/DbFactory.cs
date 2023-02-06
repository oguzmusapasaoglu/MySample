using MyCore.Dapper.Interfaces;
using System.Data.Common;
using System.Data;
using Dapper.Contrib.Extensions;
using MyCore.Dapper.Base;
using System.Linq.Expressions;
using System.Text;
using Dapper;
using MyCore.LogManager.ExceptionHandling;

namespace MyCore.Dapper.Factory
{
    public class DbFactory : IDbFactory
    {
        private IConnectionFactory connectionFactory;
        private DbConnection conn;

        public DbFactory(IConnectionFactory _connectionFactory)
        {
            connectionFactory = _connectionFactory;
        }

        public async Task<int?> InsertEntity<TEntity>(string connectionString, TEntity entity) where TEntity : BaseDapperEntity
        {
            int Pkey;
            try
            {
                using (conn = connectionFactory.CreateConnection(connectionString))
                {
                    var resultEntity = conn.Insert(entity).ToString();
                    Pkey = resultEntity.ToInt();
                    connectionFactory.CloseConnection(conn);
                    return Pkey;
                }
            }
            catch (DbException ex)
            {
                throw new FattalException(ExceptionTypeEnum.Fattal,ex, ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> InsertBulkEntity<TEntity>(string connectionString, List<TEntity> entities) where TEntity : BaseDapperEntity
        {
            try
            {
                using (conn = connectionFactory.CreateConnection(connectionString))
                {
                    foreach (var entity in entities)
                        conn.Insert(entity).ToString();

                    connectionFactory.CloseConnection(conn);
                    return true;
                }
            }
            catch (DbException ex)
            {
                throw new FattalException(ExceptionTypeEnum.Fattal,ex, ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> UpdateEntity<TEntity>(string connectionString, TEntity entity) where TEntity : BaseDapperEntity
        {
            try
            {
                bool result;
                using (conn = connectionFactory.CreateConnection(connectionString))
                {
                    result = conn.Update(entity);
                    connectionFactory.CloseConnection(conn);
                    return result;
                }
            }
            catch (DbException ex)
            {
                throw new FattalException(ExceptionTypeEnum.Fattal,ex, ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IQueryable<TEntity>> GetAllByFilter<TEntity>(string connectionString, Expression<Func<TEntity, bool>> filter)
            where TEntity : BaseDapperEntity
        {
            try
            {
                IEnumerable<TEntity> result;
                using (var conn = connectionFactory.CreateConnection(connectionString))
                {
                    result = conn.GetAll<TEntity>().AsQueryable().Where(filter);
                    connectionFactory.CloseConnection(conn);
                    return result.AsQueryable();
                }
            }
            catch (DbException ex)
            {
                throw new FattalException(ExceptionTypeEnum.Fattal,ex, ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IQueryable<TEntity>> GetAll<TEntity>(string connectionString)
            where TEntity : BaseDapperEntity
        {
            try
            {
                IEnumerable<TEntity> result;
                using (var conn = connectionFactory.CreateConnection(connectionString))
                {
                    result = conn.GetAll<TEntity>();
                    connectionFactory.CloseConnection(conn);
                    return result.AsQueryable();
                }
            }
            catch (DbException ex)
            {
                throw new FattalException(ExceptionTypeEnum.Fattal,ex, ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<TEntity> GetSingleById<TEntity>(string connectionString, int id) where TEntity : BaseDapperEntity
        {
            try
            {
                TEntity result;
                using (var conn = connectionFactory.CreateConnection(connectionString))
                {
                    result = conn.Get<TEntity>(id);
                    connectionFactory.CloseConnection(conn);
                    return result;
                }
            }
            catch (DbException ex)
            {
                throw new FattalException(ExceptionTypeEnum.Fattal,ex, ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IQueryable<TEntity>> GetData<TEntity>(string connectionString, StringBuilder queryScript, DynamicParameters parameters)
        {
            try
            {
                using (var conn = connectionFactory.CreateConnection(connectionString))
                {
                    var result = conn.Query<TEntity>(queryScript.ToString(), parameters);
                    connectionFactory.CloseConnection(conn);
                    return result.AsQueryable();
                }
            }
            catch (DbException ex)
            {
                throw new FattalException(ExceptionTypeEnum.Fattal,ex, ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IQueryable<TEntity>> GetData<TEntity>(string connectionString, StringBuilder queryScript)
        {
            try
            {
                using (var conn = connectionFactory.CreateConnection(connectionString))
                {
                    var result = conn.Query<TEntity>(queryScript.ToString());
                    connectionFactory.CloseConnection(conn);
                    return (IQueryable<TEntity>)result;
                }
            }
            catch (DbException ex)
            {
                throw new FattalException(ExceptionTypeEnum.Fattal,ex, ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<TEntity> GetSingle<TEntity>(string connectionString, StringBuilder queryScript)
        {
            try
            {
                using (var conn = connectionFactory.CreateConnection(connectionString))
                {
                    var result = conn.QueryFirst<TEntity>(queryScript.ToString());
                    connectionFactory.CloseConnection(conn);
                    return result;
                }
            }
            catch (DbException ex)
            {
                throw new FattalException(ExceptionTypeEnum.Fattal,ex, ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}