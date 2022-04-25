using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public class IndexViewModel
    {
        public List<ToDoTaskModel> CurrentTasks { get; set; }
        public List<ToDoTaskModel> CompletedTasks { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public int CurrentCategory { get; set; }

    }
}
