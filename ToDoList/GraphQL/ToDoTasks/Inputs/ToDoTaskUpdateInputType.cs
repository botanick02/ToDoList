using GraphQL.Types;

namespace ToDoList.GraphQL.ToDoTasks.Inputs
{
    public class ToDoTaskUpdateInputType : InputObjectGraphType<ToDoTaskUpdateInput>
    {
        public ToDoTaskUpdateInputType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
               .Name("Id")
               .Resolve(context => context.Source.Id);

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
