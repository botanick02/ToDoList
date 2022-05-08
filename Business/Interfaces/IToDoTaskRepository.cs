using System.Collections.Generic;

namespace Business.Models
{
    public interface IToDoTaskRepository
    {
        List<ToDoTaskModel> ListTasks(bool? isDone, int? categoryId);
        ToDoTaskModel GetTask(int id);
        bool Create(ToDoTaskModel task);
        bool Delete(int id);
        bool SetDoneStatus(int id, bool status);
        bool Update(ToDoTaskModel task);

    }
}
