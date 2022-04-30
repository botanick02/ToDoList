using System.ComponentModel.DataAnnotations;


namespace ToDoList.ViewModels.Task
{
    public class ToDoTaskCreateViewModel
    {
        public int CategoryId { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
