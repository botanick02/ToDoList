using GraphQL.Types;

namespace ToDoList.GraphQL.Storage.Inputs
{
    public class StorageeSourceChangeInputType: InputObjectGraphType<StorageeSourceChangeInput>
    {
        public StorageeSourceChangeInputType()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("StorageSource")
                .Resolve(context => context.Source.StorageSource);
        }
    }
}
