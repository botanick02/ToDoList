using System.ComponentModel.DataAnnotations;

namespace ToDoList.ViewModels
{
    public class ToDoTaskEditViewModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
        [Required(ErrorMessage = "Please enter task name")]
        public string Title { get; set; }
    }
}
