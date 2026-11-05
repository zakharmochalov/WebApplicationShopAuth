using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationShop.Data;
using WebApplicationShop.Models;

namespace WebApplicationShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? categoryId)
        {
            var vm = new ProductsIndexViewModel();

            IQueryable<Product> query = _context.Products.Include(p => p.Categories);

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.Categories.Any(c => c.Id == categoryId.Value));
            }

            vm.Products = query.ToList();
            vm.Categories = _context.Categories.ToList();
            vm.SelectedCategoryId = categoryId;

            return View(vm);
        }

        public IActionResult Create()
        {
            var vm = new ProductFormViewModel
            {
                AllCategories = _context.Categories.ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(ProductFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.AllCategories = _context.Categories.ToList();
                return View(vm);
            }

            var selectedCategories = _context.Categories.Where(c => vm.SelectedCategoryIds.Contains(c.Id)).ToList();
            vm.Product.Categories = selectedCategories;

            _context.Products.Add(vm.Product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Include(p => p.Categories).FirstOrDefault(p => p.Id == id);
            if (product == null) {return NotFound();}
            var vm = new ProductFormViewModel
            {
                Product = product,
                AllCategories = _context.Categories.ToList(),
                SelectedCategoryIds = product.Categories.Select(c => c.Id).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(ProductFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.AllCategories = _context.Categories.ToList();
                return View(vm);
            }
            var product = _context.Products.Include(p => p.Categories).FirstOrDefault(p => p.Id == vm.Product.Id);
            if (product == null){return NotFound();}
            product.Name = vm.Product.Name;
            product.Price = vm.Product.Price;
            product.Description = vm.Product.Description;
            var selectedCategories = _context.Categories.Where(c => vm.SelectedCategoryIds.Contains(c.Id)).ToList();
            product.Categories = selectedCategories;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Include(p => p.Categories).FirstOrDefault(p => p.Id == id);
            if (product == null) {return NotFound();}
            return View(product);
        }
       
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) {return NotFound();}
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}