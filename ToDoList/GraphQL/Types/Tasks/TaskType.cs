using Business.Models;
using GraphQL.Types;

namespace ToDoList.GraphQL.Types
{
    public class TaskType : ObjectGraphType<ToDoTaskModel>
    {

        public TaskType()
        {
            Field<IntGraphType>()
                .Name("Id")
                .Description("Task id")
                .Resolve(ctx => ctx.Source.Id);

            Field<StringGraphType>()
                .Name("Title")
                .Description("Task title")
                .Resolve(ctx => ctx.Source.Title);
        }
    }
}
