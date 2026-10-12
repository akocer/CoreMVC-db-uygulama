using Microsoft.AspNetCore.Mvc;
using uy01.Models;
using uy01.Repositories;
using uy01.ViewModels;

namespace uy01.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var products = _productRepository.GetList();
            var productModels = products.Select(p => new ProductModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                IsActive = p.IsActive
            }).ToList();
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
            var product = new Product();
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.IsActive = model.IsActive;
            _productRepository.Add(product);

            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var product = _productRepository.GetById(id);
            var productModel = new ProductModel();
            productModel.Id = product.Id;
            productModel.Name = product.Name;
            productModel.Description = product.Description;
            productModel.Price = product.Price;
            productModel.IsActive = product.IsActive;
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

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            var productModel = new ProductModel();
            productModel.Id = product.Id;
            productModel.Name = product.Name;
            productModel.Description = product.Description;
            productModel.Price = product.Price;
            productModel.IsActive = product.IsActive;
            return View(productModel);
         
        }

        [HttpPost]
        public IActionResult Delete(ProductModel model)
        {
            _productRepository.Delete(model.Id);
            return RedirectToAction("Index");

        }

    }
}
