using System.Collections.Generic;

namespace Business.Models
{
    public interface ICategoryRepository
    {
        List<CategoryModel> GetAllCategories();
        CategoryModel GetCategory(int id);
        bool Delete(CategoryModel category);
        bool Create(CategoryModel category);
        bool Update(CategoryModel category);
    }
}
