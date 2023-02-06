using Nest;

using MyCore.Elastic.Interfaces;

namespace MyCore.Elastic.Services
{
    public class ElasticManageServices : ElasticClientServices, IElasticManageServices
    {
        public async Task InsertAsync<T>(string connStr, string indexName, T entity)
            where T : class
        {
            try
            {
                var elasticClient = GetElasticClient(connStr);
                CheckIndex<T>(elasticClient, indexName);
                await elasticClient.IndexAsync(entity, idx => idx.Index(indexName));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task BulkInsertAsync<T>(string connStr, string indexName, IEnumerable<T> entity)
            where T : class
        {
            try
            {
                var elasticClient = GetElasticClient(connStr);
                CheckIndex<T>(elasticClient, indexName);
                await elasticClient.IndexManyAsync(entity, indexName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
