using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
{
    public class CategoriesController : Controller
    {
        private IToDoTaskRepository _taskRepository;
        private ICategoryRepository _categoryRepository;
        public CategoriesController(IToDoTaskRepository taskRep, ICategoryRepository catRep)
        {
            _taskRepository = taskRep;
            _categoryRepository = catRep;
        }
        [HttpGet]
        public ViewResult Index()
        {
            CategoriesViewModel viewModel = new CategoriesViewModel();
            viewModel.Categories = _categoryRepository.GetAllCategories();

            return View("Categories", viewModel);
        }
        [HttpGet]
        public ActionResult Delete(int categoryId)
        {
            bool res = _categoryRepository.Delete(categoryId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(CategoryCreateViewModel category)
        {
            if (ModelState.IsValid)
            {
                bool res = _categoryRepository.Create(category.Name);
            }
            return RedirectToAction("Index");
        }

    }
}
