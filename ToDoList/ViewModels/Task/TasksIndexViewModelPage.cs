using ToDoList.ViewModels;

namespace ToDoList.ViewModels
{
    public class TasksIndexViewModelPage
    {
        public List<ToDoTaskViewModel> CurrentTasks { get; set; }
        public List<ToDoTaskViewModel> CompletedTasks { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public int? CurrentCategory { get; set; }
        public ToDoTaskCreateViewModel Task { get; set; }

    }
}
