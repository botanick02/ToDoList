using Business.Models;
using GraphQL;
using GraphQL.Types;
using ToDoList.GraphQL.Storage;
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

            Field<StorageQueries>()
                 .Name("StorageSources")
                 .Resolve(_ => new { });
        }
    }
}
