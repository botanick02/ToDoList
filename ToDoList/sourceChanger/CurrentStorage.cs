using System.Diagnostics;

namespace ToDoList.sourceChanger
{
    public static class CurrentStorage
    {
        public static string CurrentSource = "Database";
        public static void SetCurrentSource(string source)
        {
            CurrentSource = source;
        }
    }
}
