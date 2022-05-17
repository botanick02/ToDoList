using GraphQL.Types;

namespace ToDoList.GraphQL
{
    public class AppSchema : Schema
    {
        public AppSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<ToDoAppQuery>();
            //Mutation = provider.GetRequiredService<ToDoAppMutations>();
        }
    }
}