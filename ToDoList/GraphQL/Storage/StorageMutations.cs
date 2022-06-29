using GraphQL;
using GraphQL.Types;
using ToDoList.sourceChanger;

namespace ToDoList.GraphQL.Storage
{
    public class StorageMutations : ObjectGraphType
    {
        public StorageMutations()
        {
            Field<StorageSourceType, StorageSourceModel>()
               .Name("SetSource")
               .Argument<NonNullGraphType<StringGraphType>, string>("Source", "Set source char")
               .Resolve(context =>
               {
                   var source = context.GetArgument<string>("Source");
                   if (CurrentStorage.SetCurrentSource(source))
                   {
                       var storage = new StorageSourceModel();
                       storage.CurrentSource = source;
                       return storage;
                   }
                   return null;
               });
        }
    }
}
