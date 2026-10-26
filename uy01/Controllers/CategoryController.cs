using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using uy01.Models;
using uy01.Repositories;
using uy01.ViewModels;

namespace uy01.Controllers
{
    public class CategoryController : Controller

    {
        private readonly CategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly INotyfService _notify;

        public CategoryController(CategoryRepository categoryRepository, IMapper mapper, INotyfService notify)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _notify = notify;
        }

        public IActionResult Index()
        {
            var categories = _categoryRepository.GetList();
            var categoryModels = _mapper.Map<List<CategoryModel>>(categories);
            return View(categoryModels);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(CategoryModel model)
        {
            var category = _mapper.Map<Category>(model);
            _categoryRepository.Add(category);
            _notify.Success("Kategori Eklendi.");

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var category = _categoryRepository.GetById(id);
            var categoryModel = _mapper.Map<CategoryModel>(category);
            return View(categoryModel);
        }

        [HttpPost]
        public IActionResult Update(CategoryModel model)
        {
            var category = _mapper.Map<Category>(model);
            _categoryRepository.Update(category);
            _notify.Success("Kategori Güncellendi.");
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var category = _categoryRepository.GetById(id);
            var categoryModel = _mapper.Map<CategoryModel>(category);
            return View(categoryModel);
        }

        [HttpPost]

        public IActionResult Delete(CategoryModel model)
        {
            _categoryRepository.Delete(model.Id);
            _notify.Success("Kategori Silindi.");
            return RedirectToAction("Index");
        }
    }
}