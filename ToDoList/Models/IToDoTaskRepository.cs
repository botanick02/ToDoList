
using ToDoList.ViewModels.Task;

namespace ToDoList.Models
{
    public interface IToDoTaskRepository
    {
        public List<ToDoTaskModel> ListItems(bool? isDone, int categoryId);
        public bool Create(ToDoTaskCreateViewModel taskCreate);

    }
}
