using RestAPI.Models;

namespace RestAPI.Services
{
    public interface IToDoRepository
    {
        List<ToDoModel> ToDos();
        ToDoModel GetToDoById(int id);
    }
}
