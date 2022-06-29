using System.Diagnostics;
using ToDoList.sourceChanger.Enums;

namespace ToDoList.sourceChanger
{
    public static class CurrentStorage
    {
        public static StorageSources CurrentSource = StorageSources.Database;

        public static bool SetCurrentSource(string source)
        {
            return Enum.TryParse(source, out CurrentSource);
        }
    }
}
