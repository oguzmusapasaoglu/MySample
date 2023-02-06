using Elasticsearch.Net;

using Nest;

namespace MyCore.Elastic.Services
{
    public abstract class ElasticClientServices
    {
        internal ElasticClient GetElasticClient(string connStr)
        {
            var node = new SingleNodeConnectionPool(new Uri(connStr));
            var settings = new ConnectionSettings(node);
            return new ElasticClient(settings);
        }

        internal void CheckIndex<T>(ElasticClient client, string indexName) where T : class
        {
            var response = client.Indices.Exists(indexName);
            if (!response.Exists)
            {
                client.Indices.Create(indexName, index =>
                   index.Mappings(ms =>
                       ms.Map<T>(x => x.AutoMap())));
            }
        }
    }
}
