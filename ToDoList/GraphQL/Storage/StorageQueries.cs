using GraphQL.Types;
using ToDoList.sourceChanger;

namespace ToDoList.GraphQL.Storage
{
    public class StorageQueries : ObjectGraphType
    {
        private StorageSourcesProvider storageSourcesProvider;
        public StorageQueries(StorageSourcesProvider storageProvider)
        {
            storageSourcesProvider = storageProvider;

            Field<NonNullGraphType<StorageSourceType>>()
                .Name("GetStorageSourcesData")
                .Resolve(context =>
                {
                    var data = new StorageSourceModel();

                    data.StorageSources = storageSourcesProvider.GetStorageSourcesNames();
                    data.CurrentSource = storageSourcesProvider.GetCurrentSourceName();
                    return data;
                });
        }


    }
}
