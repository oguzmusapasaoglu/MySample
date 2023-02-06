using MyCore.Dapper.Interfaces;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using MyCore.LogManager.ExceptionHandling;

namespace MyCore.Dapper.Factory
{
    public class ConnectionFactory : IConnectionFactory
    {
        public SqlConnection CreateConnection(string connectionStr)
        {
            try
            {
                var connection = new SqlConnection(connectionStr);
                if (connection.State == ConnectionState.Open)
                    connection.Close();

                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                throw new KnownException(ExceptionTypeEnum.Fattal, ex);
            }
        }
        public void CloseConnection(DbConnection conn)
        {
            try
            {
                conn.Close();
                conn.Dispose();
            }
            catch (DbException ex)
            {
                throw new KnownException(ExceptionTypeEnum.Fattal, ex);
            }
        }
    }
}
