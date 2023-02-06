using Dapper.Contrib.Extensions;

namespace MyCore.DapperHelper
{
    internal class DapperHelper
    {
        public static string GetTableName<T>()
        {
            var tAttribute = (TableAttribute)typeof(T).GetCustomAttributes(typeof(TableAttribute), true)[0];
            string tableName = tAttribute.Name;
            return tableName;
        }
    }
}
