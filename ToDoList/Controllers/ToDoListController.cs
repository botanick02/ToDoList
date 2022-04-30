using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Models;
using ToDoList.ViewModels;
using ToDoList.ViewModels.Task;


namespace ToDoList.Controllers
{
    public class ToDoListController : Controller
    {
        private IToDoTaskRepository _taskRepository;
        private ICategoryRepository _categoryRepository;
        public ToDoListController(IToDoTaskRepository taskRep, ICategoryRepository catRep)
        {
            _taskRepository = taskRep;
            _categoryRepository = catRep;
        }

        [HttpGet]
        public ViewResult Index(int categoryId)
        {
            IndexViewModel viewModel = new IndexViewModel();
            viewModel.CurrentTasks = _taskRepository.ListItems(isDone: false, categoryId: categoryId);
            viewModel.CompletedTasks = _taskRepository.ListItems(isDone: true, categoryId: categoryId);
            viewModel.Categories = _categoryRepository.GetAllCategories();
            viewModel.CurrentCategory = categoryId;
            return View("Index", viewModel);
		}
        //public ViewResult Categories()
        //{
        //    CategoriesViewModel viewModel = new CategoriesViewModel();
        //    viewModel.Categories = _categoryRepository.GetAllCategories();
        //    return View("Categories", viewModel);
        //}
        [HttpPost]
        public IActionResult Create(ToDoTaskCreateViewModel taskCreate)
        {

            if (ModelState.IsValid)
            {
                bool res = _taskRepository.Create(taskCreate);
                Debug.WriteLine("category = " + taskCreate.CategoryId);

            }
            return RedirectToAction("Index");
        }
    }
}
