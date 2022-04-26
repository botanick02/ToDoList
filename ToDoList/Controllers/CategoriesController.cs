using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
{
    public class CategoriesController : Controller
    {
        private IConfiguration _configuration;
        public CategoriesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ViewResult Index()
        {
            CategoryDBHandle categoryDbHandle = new CategoryDBHandle(_configuration);
            CategoriesViewModel viewModel = new CategoriesViewModel();
            viewModel.Categories = categoryDbHandle.ListCategories();

            return View("Categories", viewModel);
        }
        [HttpGet]
        public ActionResult Delete(int categoryId)
        {
            CategoryDBHandle categoryDbHandle = new CategoryDBHandle(_configuration);
            bool res = categoryDbHandle.Delete(categoryId);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Create(string name)
        {
            Debug.WriteLine($"CreateController - {name}");

            if (ModelState.IsValid)
            {
                CategoryDBHandle categoryDbHandle = new CategoryDBHandle(_configuration);
                bool res = categoryDbHandle.Create(name);
            }
            return RedirectToAction("Index");
        }

    }
}
