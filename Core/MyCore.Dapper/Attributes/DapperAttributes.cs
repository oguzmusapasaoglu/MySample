using Dapper.Contrib.Extensions;

namespace MyCore.Dapper.Attributes
{
    public class DapperKey : KeyAttribute
    {
    }
    public class DapperTable : TableAttribute
    {
        public DapperTable(string tableName) : base(tableName)
        {
        }
    }
    public class DapperWrite : WriteAttribute
    {
        public DapperWrite(bool write) : base(write)
        {
        }
    }
}
