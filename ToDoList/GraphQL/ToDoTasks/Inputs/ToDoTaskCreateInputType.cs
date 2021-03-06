using Business.Models;
using GraphQL.Types;

namespace ToDoList.GraphQL.ToDoTasks.Inputs
{
    public class ToDoTaskCreateInputType : InputObjectGraphType<ToDoTaskCreateInput>
    {
        public ToDoTaskCreateInputType()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Title")
                .Resolve(context => context.Source.Title);

            Field<NonNullGraphType<IntGraphType>, int>()
                    .Name("CategoryId")
                    .Resolve(context => context.Source.CategoryId);

            Field<DateTimeGraphType, DateTime?>()
                    .Name("DeadlineDate")
                    .Resolve(context => context.Source.DeadlineDate);
        }
        
    }
}
