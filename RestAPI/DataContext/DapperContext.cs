using MySql.Data.MySqlClient;
using System.Data;

namespace RestAPI.DataContext
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_configuration.GetConnectionString("MySqlConnection"));
        }
    }
}