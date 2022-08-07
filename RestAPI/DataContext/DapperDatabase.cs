using Dapper;
using System.Data;

namespace RestAPI.DataContext
{
    public class DapperDatabase
    {
        private readonly DapperContext _context;

        public DapperDatabase(DapperContext context)
        {
            _context = context;
        }

        public IDbConnection CreateDatabase(string dbName)
        {
            var query = "Select * From sys.databases WHERE name = @name";
            var parameters = new DynamicParameters();
            parameters.Add(dbName);

            using(var connection = _context.CreateConnection())
            {
                var records = connection.Query(query, parameters);
                if (!records.Any())
                {
                    connection.Execute($"Create Database {dbName}");
                }
                return connection;
            }
        }
    }
}
