using GraphQL.Types;

namespace ToDoList.GraphQL.Storage
{
    public class StorageSourceType: ObjectGraphType<StorageSourceModel>
    {
        public StorageSourceType()
        {
            Field<NonNullGraphType<StringGraphType>, string>()
                .Name("CurrentSource")
                .Resolve(ctx => ctx.Source.CurrentSource);

            Field<NonNullGraphType<ListGraphType<StringGraphType>>, string[]>()
                .Name("StorageSources")
                .Resolve(ctx => ctx.Source.StorageSources);
        }
    }
}
