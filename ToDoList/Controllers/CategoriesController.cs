using Microsoft.AspNetCore.Mvc;
using ToDoList.ViewModels;
using AutoMapper;
using Business.Models;

namespace ToDoList.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        public CategoriesController(ICategoryRepository catRep, IMapper mapper)
        {
            categoryRepository = catRep;
            this.mapper = mapper;
        }
        [HttpGet]
        public ViewResult Index()
        {
            CategoriesViewModelPage viewModel = new CategoriesViewModelPage();
            var categories = categoryRepository.GetAllCategories();
            viewModel.Categories = mapper.Map<List<CategoryViewModel>>(categories);

            return View("Categories", viewModel);
        }
        [HttpPost]
        public ActionResult Delete(CategoryDeleteViewModel category)
        {
            if (ModelState.IsValid)
            {
                var categoryModel = mapper.Map<CategoryModel>(category);
                bool res = categoryRepository.Delete(categoryModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(CategoryCreateViewModel category)
        {
            if (ModelState.IsValid)
            {
                var categoryModel = mapper.Map<CategoryModel>(category);
                bool res = categoryRepository.Create(categoryModel);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            if (ModelState.IsValid)
            {
                var categoryModel = categoryRepository.GetCategory(id);
                var viewModdelPage = mapper.Map<CategoryViewModel>(categoryModel);
                return View("Edit", viewModdelPage);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Update(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                var categoryModel = mapper.Map<CategoryModel>(category);
                var res = categoryRepository.Update(categoryModel);
                if (res)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Edit", category);

            }
            return RedirectToAction("Edit", category);
        }
    }
}
