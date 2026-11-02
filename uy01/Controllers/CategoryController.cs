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

        public async Task<IActionResult> IndexAsync()
        {
            var categories =await _categoryRepository.GetAllAsync();
            var categoryModels = _mapper.Map<List<CategoryModel>>(categories);
            return View(categoryModels);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> Add(CategoryModel model)
        {
            if (_categoryRepository.Where(s => s.Name == model.Name).Count() > 0)
            {
                _notify.Error("Girilen Kategori Adı Kayıtlıdır!");
                return View(model);
            }
            
            
            var category = _mapper.Map<Category>(model);
           await  _categoryRepository.AddAsync(category);
            _notify.Success("Kategori Eklendi.");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateAsync(int id)
        {
            var category =await _categoryRepository.GetByIdAsync(id);
            var categoryModel = _mapper.Map<CategoryModel>(category);
            return View(categoryModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(CategoryModel model)
        {
            var category = _mapper.Map<Category>(model);
            await _categoryRepository.UpdateAsync(category);
            _notify.Success("Kategori Güncellendi.");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category =await  _categoryRepository.GetByIdAsync(id);
            var categoryModel = _mapper.Map<CategoryModel>(category);
            return View(categoryModel);
        }

        [HttpPost]

        public async Task<IActionResult> DeleteAsync(CategoryModel model)
        {
           await _categoryRepository.DeleteAsync(model.Id);
            _notify.Success("Kategori Silindi.");
            return RedirectToAction("Index");
        }
    }
}