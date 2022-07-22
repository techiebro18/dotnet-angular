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
            ToDoModel task = new ToDoModel();
            using (var connection = new MySqlConnection(_db.getDBConnectionString()))
            {
                task = connection.QuerySingleOrDefault<ToDoModel>("Select * From tasks Where Id=@Id", new { Id = id });
                return task;
            }
        }

        public int AddNewTask(ToDoModel todoModel)
        {
            var task = new { 
                Title = todoModel.Title,
            };
            using (var connection = new MySqlConnection(_db.getDBConnectionString()))
            {
                var affectedRows = connection.Execute("Insert into tasks (Title) values (@Title)", task);
                return affectedRows;
            }
        }

        public int UpdateTask(ToDoModel toDoModel, int id)
        {
            var task = new {
                Id = id,
                Title = toDoModel.Title
            };
            using ( var connection = new MySqlConnection(_db.getDBConnectionString()))
            {
                var affectedRows = connection.Execute("Update tasks set Title = @Title Where Id = @Id", task);
                return affectedRows;
            }
        }

        public int DeleteTask(int id)
        {
            using (var connection = new MySqlConnection(_db.getDBConnectionString()))
            {
                var affectedRows = connection.Execute("Delete from tasks Where Id = @Id", new { Id = id });
                return affectedRows;
            }
        }
    }
}
