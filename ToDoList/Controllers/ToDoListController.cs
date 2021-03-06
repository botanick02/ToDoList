using AutoMapper;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.sourceChanger;
using ToDoList.ViewModels;
using GraphQL;
using GraphQL.Types;

namespace ToDoList.Controllers
{
    public class ToDoListController : Controller
    {
        private IToDoTaskRepository taskRepository;
        private ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public ToDoListController(ToDoTaskRepositoryResolver taskRep, CategoryRepositoryResolver catRep, IMapper mapper)
        {
            taskRepository = taskRep(CurrentStorage.CurrentSource);
            categoryRepository = catRep(CurrentStorage.CurrentSource);
            this.mapper = mapper;
        }

        [HttpGet]
        public ViewResult Index(int? categoryId = null)
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
                var res = taskRepository.Create(taskModel);
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
        public IActionResult ToggleDoneStatus(int id)
        {
            if (ModelState.IsValid)
            {
                var res = taskRepository.ToggleDoneStatus(id);
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
                pageViewModel.IsDone = taskDetails.IsDone;
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
                var res = taskRepository.Update(taskModel);
                if (res != null)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Edit", task);

            }
            return RedirectToAction("Edit", task);
        }


        private TasksIndexViewModelPage PrepareViewModelForIndex(int? categoryId = null)
        {
            TasksIndexViewModelPage viewModelPage = new TasksIndexViewModelPage();
            var currentTasks = taskRepository.ListTasks(false, categoryId);
            var completedTasks = taskRepository.ListTasks(true, categoryId);
            var categories = categoryRepository.GetAllCategories();

            viewModelPage.CurrentTasks = mapper.Map<List<ToDoTaskViewModel>>(currentTasks);
            viewModelPage.CompletedTasks = mapper.Map<List<ToDoTaskViewModel>>(completedTasks);
            viewModelPage.Categories = mapper.Map<List<CategoryViewModel>>(categories);
            viewModelPage.CurrentCategory = categoryId;
            return viewModelPage;
        }
    }

}
