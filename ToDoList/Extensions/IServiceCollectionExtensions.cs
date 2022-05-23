using GraphQL;
using GraphQL.Server;
using ToDoList.GraphQL;

namespace ToDoList.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddGraphQLApi(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            
            services.AddSingleton<DoToListSchema>();
            services
                .AddGraphQL()
                .AddSystemTextJson()
                .AddGraphTypes(typeof(DoToListSchema));
            return services;
        }
    }
}
