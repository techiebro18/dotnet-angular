using RestAPI.Mappings;
using RestAPI.Models;

namespace RestAPI.Services
{
    public interface IToDoRepository
    {
        List<ToDoViewModel> ToDos();
        ToDoModel GetToDoById(int id);
        int AddNewTask(ToDoModel todoModel);
        int UpdateTask(ToDoModel toDoModel, int id);
        int DeleteTask(int id);
    }
}
