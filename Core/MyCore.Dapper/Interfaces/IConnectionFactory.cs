using System.Data.Common;
using System.Data.SqlClient;

namespace MyCore.Dapper.Interfaces
{
    public interface IConnectionFactory
    {
        SqlConnection CreateConnection(string connectionStr);
        void CloseConnection(DbConnection conn);
    }
}
