using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using uy01.Repositories;
using uy01.ViewModels;

namespace uy01.Controllers
{
    public class CategoryController:Controller

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
    }
}
