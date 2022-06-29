using ToDoList.sourceChanger.Enums;

namespace ToDoList.sourceChanger
{
    public class StorageSourcesProvider
    {
        public String GetCurrentSourceName()
        {
            return CurrentStorage.CurrentSource.ToString();
        }
        public String[] GetStorageSourcesNames()
        {
            var sources = new List<String>();
            foreach (StorageSources source in (StorageSources[])Enum.GetValues(typeof(StorageSources)))
            {
                sources.Add(source.ToString());
            }
            return sources.ToArray();
        }
    }
}
