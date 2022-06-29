using System.Collections.Generic;

namespace Business.Models
{
    public interface ICategoryRepository
    {
        List<CategoryModel> GetAllCategories();
        CategoryModel GetCategory(int id);
        bool Delete(int id);
        CategoryModel Create(CategoryModel category);
        CategoryModel Update(CategoryModel category);
    }
}
