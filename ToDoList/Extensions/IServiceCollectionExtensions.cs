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
            
            services.AddSingleton<AppSchema>();
            services
                .AddGraphQL()
                .AddSystemTextJson()
                .AddGraphTypes(typeof(AppSchema));
            return services;
        }
    }
}
