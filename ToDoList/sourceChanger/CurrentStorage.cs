namespace ToDoList.sourceChanger
{
    public static class CurrentStorage
    {
        public static string CurrentSource = "D";
        public static void SetCurrentSource(string source)
        {
            CurrentSource = source;
        }
    }
}
