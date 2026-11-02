using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using uy01.Models;
using uy01.Repositories;
using uy01.ViewModels;

namespace uy01.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly INotyfService _notify;

        public ProductController(ProductRepository productRepository, IMapper mapper, INotyfService notify, CategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _notify = notify;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            var products =await _productRepository.GetAllAsync();
            var productModels = _mapper.Map<List<ProductModel>>(products);

            foreach (var product in productModels)
            {
                var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
                product.CategoryName = category.Name;
            }
            return View(productModels);
        }
        public async Task<IActionResult> AddAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoryList = categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            ViewBag.Categories = categoryList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }

            var products = await _productRepository.Where(s=>s.Name==model.Name && s.CategoryId==model.CategoryId).ToListAsync();
            if (products.Count() > 0)
            {
                _notify.Error(model.Name + " adlı ürün kayıtlıdır!");
                return View(model);
            }
            var product = new Product();
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.IsActive = model.IsActive;
            product.CategoryId = model.CategoryId;
            await _productRepository.AddAsync(product);
            _notify.Success("Ürün Eklendi");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            var productModel = _mapper.Map<ProductModel>(product);

            var categories = await _categoryRepository.GetAllAsync();
            var categoryList = categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            ViewBag.Categories = categoryList;

            return View(productModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            var product =await _productRepository.GetByIdAsync(model.Id);
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.IsActive = model.IsActive;
            product.CategoryId = model.CategoryId;
            await _productRepository.UpdateAsync(product);
            _notify.Success("Ürün Güncellendi");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var product =await _productRepository.GetByIdAsync(id);
            var productModel = _mapper.Map<ProductModel>(product);
            return View(productModel);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(ProductModel model)
        {
          await  _productRepository.DeleteAsync(model.Id);
            _notify.Success("Ürün Silindi");
            return RedirectToAction("Index");

        }

    }
}
