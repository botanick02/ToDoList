using System.Collections.Generic;

namespace Business.Models
{
    public interface IToDoTaskRepository
    {
        List<ToDoTaskModel> ListTasks(bool? isDone = null, int? categoryId = null);
        ToDoTaskModel GetTask(int id);
        ToDoTaskModel Create(ToDoTaskModel task);
        bool Delete(int id);
        ToDoTaskModel ToggleDoneStatus(int id);
        ToDoTaskModel Update(ToDoTaskModel task);

    }
}
