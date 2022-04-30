namespace ToDoList.Models
{
    public interface ICategoryRepository 
    {
        public List<CategoryModel> GetAllCategories();
        public bool Delete(int categoryId);
        public bool Create(string name);
    }
}
