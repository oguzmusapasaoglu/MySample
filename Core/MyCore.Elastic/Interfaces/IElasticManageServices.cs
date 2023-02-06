namespace MyCore.Elastic.Interfaces
{
    public interface IElasticManageServices
    {
        Task InsertAsync<T>(string connStr, string indexName, T entity) where T : class;
        Task BulkInsertAsync<T>(string connStr, string indexName, IEnumerable<T> entity) where T : class;
    }
}