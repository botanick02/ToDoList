using GraphQL.Types;

namespace ToDoList.GraphQL
{
    public class DoToListSchema : Schema
    {
        public DoToListSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<RootQueries>();
            Mutation = provider.GetRequiredService<RootMutations>();
        }
    }
}