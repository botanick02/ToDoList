namespace ToDoList.ViewModels
{
    public class ToDoTaskEditViewModelPage
    {
        public ToDoTaskEditViewModel ToDoTaskEditViewModel { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public bool IsDone { get; set; }
    }
}
