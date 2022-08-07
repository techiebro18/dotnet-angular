namespace RestAPI.DataContext
{
    public class Database
    {
        private readonly IConfiguration _configuration;

        public Database(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string getDBConnectionString()
        {
            //return "Server=127.0.0.1;Database=todo;Uid=root;Pwd=;";
            //return _configuration["ConnectionStrings:DefaultConnection"];
            return _configuration.GetConnectionString("DefaultConnection");
        }
    }
}
