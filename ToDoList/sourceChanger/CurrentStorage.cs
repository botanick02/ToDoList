namespace ToDoList.sourceChanger
{
    public static class CurrentStorage
    {
        public static string CurrentSource = "X";
        public static void SetCurrentSource(string source)
        {
            CurrentSource = source;
        }
    }
}
