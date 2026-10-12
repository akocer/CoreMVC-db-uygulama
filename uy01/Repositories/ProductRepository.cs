using uy01.Models;

namespace uy01.Repositories
{
    public class ProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Product> GetList()
        {
            return _context.Products.ToList();
        }
        public Product GetById(int id)
        {
            return _context.Products.Where(s => s.Id == id).SingleOrDefault();
        }
        public void Add(Product model)
        {
            _context.Products.Add(model);
            _context.SaveChanges();
        }

        public void Update(Product model)
        {
            var product = _context.Products.Where(s => s.Id == model.Id).SingleOrDefault();
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.IsActive = model.IsActive;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Products.Where(s => s.Id == id).SingleOrDefault();
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
