namespace ToDoList.GraphQL.ToDoTasks.Inputs
{
    public class ToDoTaskCreateInput
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public DateTime? DeadlineDate { get; set; }

    }
}
