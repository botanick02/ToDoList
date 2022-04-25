using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
{
    public class ToDoListController : Controller
    {
        private IConfiguration _configuration;
        public ToDoListController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ViewResult Index(int categoryId)
        {
            ToDoTaskDBHandle taskDbHandle = new ToDoTaskDBHandle(_configuration);
            CategoryDBHandle categoryDbHandle = new CategoryDBHandle(_configuration);
            IndexViewModel viewModel = new IndexViewModel();
            viewModel.CurrentTasks = taskDbHandle.ListItems(isDone: false, categoryId: categoryId);
            viewModel.CompletedTasks = taskDbHandle.ListItems(isDone: true, categoryId: categoryId);
            viewModel.Categories = categoryDbHandle.ListCategories();
            viewModel.CurrentCategory = categoryId;
            return View("Index", viewModel);
		}
        public ViewResult Categories()
        {
            CategoryDBHandle categoryDbHandle = new CategoryDBHandle(_configuration);
            CategoriesViewModel viewModel = new CategoriesViewModel();
            viewModel.Categories = categoryDbHandle.ListCategories();
            return View("Categories", viewModel);
        }
    }
}
