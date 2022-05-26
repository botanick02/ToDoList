using GraphQL.Types;
using ToDoList.GraphQL.Storage;
using ToDoList.GraphQL.ToDoTasks;

namespace ToDoList.GraphQL
{
    public class RootMutations : ObjectGraphType
    {
        public RootMutations()
        {

            Field<ToDoTasksMutations>()
                .Name("ToDoTasks")
                .Resolve(_ => new { });

            Field<CategoriesMutations>()
               .Name("Categories")
               .Resolve(_ => new { });

            Field<StorageMutations>()
               .Name("Storage")
               .Resolve(_ => new { });
        }
    }
}
