namespace ToDoList.GraphQL.ToDoTasks.Inputs
{
    public class ToDoTaskUpdateInput 
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public string Title { get; set; }
    }
}
