using uy01.Models;

namespace uy01.Repositories
{
    public class CategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Category> GetList()
        {
            return _context.Categories.ToList();
        }
        public Category GetById(int id)
        {
            return _context.Categories.Where(s => s.Id == id).SingleOrDefault();
        }
        public void Add(Category model)
        {
            _context.Categories.Add(model);
            _context.SaveChanges();
        }

        public void Update(Category model)
        {
            var category = _context.Categories.Where(s => s.Id == model.Id).SingleOrDefault();
            category.Name = model.Name;
            category.IsActive = model.IsActive;
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = _context.Categories.Where(s => s.Id == id).SingleOrDefault();
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
