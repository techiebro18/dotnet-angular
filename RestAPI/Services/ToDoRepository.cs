using RestAPI.Models;
using Dapper;
using MySql.Data.MySqlClient;
using RestAPI.DataContext;
using RestAPI.Mappings;
using AutoMapper;

namespace RestAPI.Services
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly Database _db;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ToDoRepository(IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _db = new Database(_configuration);
            _mapper = mapper;
        }
        public List<ToDoViewModel> ToDos()
        {
            List<ToDoViewModel> todos;
            //List<ToDoModel> todos = new List<ToDoModel>();
            using (var connection = new MySqlConnection(_db.getDBConnectionString()))
            {
                var tasks = connection.Query<ToDoModel>("SELECT * FROM tasks");
                /*foreach (var task in tasks)
                {
                    //Auto mapper can be used here
                    todos.Add(new ToDoModel { Id = task.Id, Title = task.Title });
                }*/
                todos = _mapper.Map<List<ToDoViewModel>>(tasks);
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
