using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationShop.Data;
using WebApplicationShop.Models;

namespace WebApplicationShop.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;
        public CategoriesController(AppDbContext context) => _context = context;
        public IActionResult Index() => View(_context.Categories.ToList());
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (_context.Categories.Any(c => c.Name == category.Name))
            {
                ModelState.AddModelError("Name", "Категория с таким названием уже существует");
            }
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (_context.Categories.Any(c => c.Id != category.Id && c.Name == category.Name))
            {
                ModelState.AddModelError("Name", "Категория с таким названием уже существует");
            }
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _context.Categories.Update(category);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            var category = _context.Categories
                .Include(c => c.Products)
                .ThenInclude(p => p.Categories)
                .FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            foreach (var product in category.Products.ToList())
            {
                product.Categories.Remove(category);
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
