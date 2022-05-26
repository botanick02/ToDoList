using GraphQL;
using GraphQL.Types;
using ToDoList.sourceChanger;

namespace ToDoList.GraphQL.Storage
{
    public class StorageMutations : ObjectGraphType
    {
        public StorageMutations()
        {
            Field<NonNullGraphType<StringGraphType>>()
               .Name("SetSource")
               .Argument<NonNullGraphType<StringGraphType>, string>("Source", "Set source char")
               .Resolve(context =>
               {
                   var source = context.GetArgument<string>("Source");
                   if (CurrentStorage.SetCurrentSource(source))
                   {
                       return source;
                   }
                   return "Error: incorrect value";
               });
        }
    }
}
