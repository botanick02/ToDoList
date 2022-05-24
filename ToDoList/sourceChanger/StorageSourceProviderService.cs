using MicrosoftSqlDB.Models;
using ToDoList.sourceChanger.Enums;
using XMLStorage;

namespace ToDoList.sourceChanger
{
    public static class StorageSourceProviderService
    {
        public static IServiceCollection AddProviderService(this IServiceCollection services)
        {
            services.AddTransient<ToDoTaskDBRepository>();
            services.AddTransient<CategoryDBRepository>();
            services.AddTransient<ToDoTaskXMLRepository>();
            services.AddTransient<CategoryXMLRepository>();

            services.AddTransient<CategoryRepositoryResolver>(CategoryRepositoryProvider => key =>
            {
                StorageSources source;
                if (!Enum.TryParse(key, out source))
                {
                    source = StorageSources.Database;
                }

                switch (source)
                {
                    case StorageSources.Database:
                        return CategoryRepositoryProvider.GetService<CategoryDBRepository>();
                    case StorageSources.XML:
                        return CategoryRepositoryProvider.GetService<CategoryXMLRepository>();
                    default:
                        throw new KeyNotFoundException();
                }
            });
            services.AddTransient<ToDoTaskRepositoryResolver>(ToDoTaskRepositoryProvider => key =>
            {
                StorageSources source;
                if (!Enum.TryParse(key, out source))
                {
                    source = StorageSources.Database;
                }

                switch (source)
                {
                    case StorageSources.Database:
                        return ToDoTaskRepositoryProvider.GetService<ToDoTaskDBRepository>();
                    case StorageSources.XML:
                        return ToDoTaskRepositoryProvider.GetService<ToDoTaskXMLRepository>();
                    default:
                        throw new KeyNotFoundException();
                }
            });
            return services;
        }
    }
}
