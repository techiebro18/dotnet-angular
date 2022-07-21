using RestAPI.Models;
using Dapper;
using MySql.Data.MySqlClient;
using RestAPI.DataContext;

namespace RestAPI.Services
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly Database _db;

        public ToDoRepository()
        {
            _db = new Database();
        }
        public List<ToDoModel> ToDos()
        {
            List<ToDoModel> todos = new List<ToDoModel>();
            using (var connection = new MySqlConnection(_db.getDBConnectionString()))
            {
                var tasks = connection.Query<ToDoModel>("SELECT * FROM tasks");
                foreach (var task in tasks)
                {
                    //Auto mapper can be used here
                    todos.Add(new ToDoModel { Id = task.Id, Title = task.Title });
                }
            }
            return todos;
        }

        public ToDoModel GetToDoById(int id)
        {
            return new ToDoModel { Id = 3, Title = "Testing" };
        }
    }
}
