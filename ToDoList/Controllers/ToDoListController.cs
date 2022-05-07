using AutoMapper;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.ViewModels;


namespace ToDoList.Controllers
{
    public class ToDoListController : Controller
    {
        private IToDoTaskRepository taskRepository;
        private ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public ToDoListController(IToDoTaskRepository taskRep, ICategoryRepository catRep, IMapper mapper)
        {
            taskRepository = taskRep;
            categoryRepository = catRep;
            this.mapper = mapper;
        }

        [HttpGet]
        public ViewResult Index(int categoryId)
        {
            var viewModelPage = PrepareViewModelForIndex(categoryId);
            return View("Index", viewModelPage);
        }
        [HttpPost]
        public IActionResult Create(ToDoTaskCreateViewModel task)
        {
            if (ModelState.IsValid)
            {
                var taskModel = mapper.Map<ToDoTaskModel>(task);
                bool res = taskRepository.Create(taskModel);
            }
            var viewModelPage = PrepareViewModelForIndex();
            return View("Index", viewModelPage);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                bool res = taskRepository.Delete(id);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult MarkAsDone(int id)
        {
            if (ModelState.IsValid)
            {
                bool res = taskRepository.SetDoneStatus(id, true);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult MarkAsNotDone(int id)
        {
            if (ModelState.IsValid)
            {
                bool res = taskRepository.SetDoneStatus(id, false);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(int id)
        {
            if (ModelState.IsValid)
            {
                var pageViewModel = new ToDoTaskEditViewModelPage();

                var taskDetails = taskRepository.GetTask(id);
                var categories = categoryRepository.GetAllCategories();
                pageViewModel.ToDoTaskEditViewModel = mapper.Map<ToDoTaskEditViewModel>(taskDetails);
                pageViewModel.Categories = mapper.Map<List<CategoryViewModel>>(categories);
                return View("Edit", pageViewModel);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Update(ToDoTaskEditViewModel task)
        {

            if (ModelState.IsValid)
            {
                var taskModel = mapper.Map<ToDoTaskModel>(task);
                bool res = taskRepository.Update(taskModel);
                Debug.WriteLine(res);
                if (res)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Edit", task);

            }
            return RedirectToAction("Edit", task);
        }


        private TasksIndexViewModelPage PrepareViewModelForIndex(int categoryId = 1)
        {
            TasksIndexViewModelPage viewModelPage = new TasksIndexViewModelPage();
            var currentTasks = taskRepository.ListItems(isDone: false, categoryId: categoryId);
            var completedTasks = taskRepository.ListItems(isDone: true, categoryId: categoryId);
            var categories = categoryRepository.GetAllCategories();

            viewModelPage.CurrentTasks = mapper.Map<List<ToDoTaskViewModel>>(currentTasks);
            viewModelPage.CompletedTasks = mapper.Map<List<ToDoTaskViewModel>>(completedTasks);
            viewModelPage.Categories = mapper.Map<List<CategoryViewModel>>(categories);
            viewModelPage.CurrentCategory = categoryId;
            return viewModelPage;
        }

      
    }
}
