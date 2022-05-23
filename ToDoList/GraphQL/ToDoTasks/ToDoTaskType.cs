using Business.Models;
using GraphQL.Types;

namespace ToDoList.GraphQL.ToDoTasks
{
    public class ToDoTaskType : ObjectGraphType<ToDoTaskModel>
    {
        public ToDoTaskType()
        {
            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("Id")
                .Resolve(ctx => ctx.Source.Id);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Title")
                .Resolve(ctx => ctx.Source.Title);

            Field<NonNullGraphType<IntGraphType>, int>()
                .Name("CategoryId")
                .Resolve(ctx => ctx.Source.CategoryId);

            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("Category")
                .Resolve(ctx => ctx.Source.Category);

            Field<NonNullGraphType<DateTimeGraphType>, DateTime>()
                .Name("CreatedDate")
                .Resolve(ctx => ctx.Source.CreatedDate);

            Field<BooleanGraphType, bool>()
                .Name("IsDone")
                .Resolve(ctx => ctx.Source.IsDone);

            Field<DateTimeGraphType, DateTime?>()
                .Name("DeadlineDate")
                .Resolve(ctx => ctx.Source.DeadlineDate);

            Field<DateTimeGraphType, DateTime?>()
               .Name("DoneDate")
               .Resolve(ctx => ctx.Source.DoneDate);
        }
    }
}
