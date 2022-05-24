using System.Collections.Generic;

namespace Business.Models
{
    public interface IToDoTaskRepository
    {
        List<ToDoTaskModel> ListTasks(bool? isDone = null, int? categoryId = null);
        ToDoTaskModel GetTask(int id);
        bool Create(ToDoTaskModel task);
        bool Delete(int id);
        bool ToggleDoneStatus(int id);
        bool Update(ToDoTaskModel task);

    }
}
