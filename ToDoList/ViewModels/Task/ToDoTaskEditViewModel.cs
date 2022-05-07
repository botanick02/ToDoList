using System.ComponentModel.DataAnnotations;

namespace ToDoList.ViewModels
{
    public class ToDoTaskEditViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public string Title { get; set; }
    }
}
