using Business.Models;
using GraphQL;
using GraphQL.Types;
using ToDoList.GraphQL.ToDoTasks;
using ToDoList.sourceChanger;

namespace ToDoList.GraphQL
{
    public class RootQueries : ObjectGraphType
    {
        public RootQueries(ToDoTaskRepositoryResolver taskRep)
        {

            Field<ToDoTasksQueries>()
               .Name("ToDoTasks")
               .Resolve(_ => new { });

            Field<CategoriesQueries>()
                .Name("Categories")
                .Resolve(_ => new { });

            Field<NonNullGraphType<StringGraphType>>()
                .Name("SetSource")
                .Argument<NonNullGraphType<StringGraphType>, string>("Source", "Set source char")
                .Resolve(context =>
                {
                    var source = context.GetArgument<string>("Source");
                    CurrentStorage.SetCurrentSource(source);
                    return source;
                });
        }
    }
}
