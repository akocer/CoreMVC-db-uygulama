using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using uy01.Models;
using uy01.Repositories;
using uy01.ViewModels;

namespace uy01.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly INotyfService _notify;

        public ProductController(ProductRepository productRepository, IMapper mapper, INotyfService notify)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _notify = notify;
        }
        public IActionResult Index()
        {
            var products = _productRepository.GetList();
            var productModels = _mapper.Map<List<ProductModel>>(products);
            return View(productModels);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }

            var products = _productRepository.GetList();
            if (products.Count(c => c.Name == model.Name) > 0)
            {
                _notify.Error(model.Name + " adlı ürün kayıtlıdır!");
                return View(model);
            }
            var product = new Product();
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.IsActive = model.IsActive;
            _productRepository.Add(product);
            _notify.Success("Ürün Eklendi");
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var product = _productRepository.GetById(id);
            var productModel = _mapper.Map<ProductModel>(product);
            return View(productModel);
        }

        [HttpPost]
        public IActionResult Update(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }
            var product = _productRepository.GetById(model.Id);
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.IsActive = model.IsActive;

            _productRepository.Update(product);
            _notify.Success("Ürün Güncellendi");
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            var productModel = _mapper.Map<ProductModel>(product);
            return View(productModel);
         
        }

        [HttpPost]
        public IActionResult Delete(ProductModel model)
        {
            _productRepository.Delete(model.Id);
            _notify.Success("Ürün Silindi");
            return RedirectToAction("Index");

        }

    }
}
