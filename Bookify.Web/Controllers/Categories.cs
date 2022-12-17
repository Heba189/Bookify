
using Bookify.Web.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Web.Controllers
{
    public class Categories : Controller
    {
        private readonly ApplicationDbContext _context;

        public Categories(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
          return View("Form");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
             return View("Form", model);

            var category = new Category { Name = model.Name };
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var category = _context.Categories.Find(Id);
            if (category is null)
                return NotFound();

            var viewModel = new CategoryFormViewModel
            {
                Id = Id,
                Name = category.Name
            };
            return View("Form", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Form", model);

            var category = _context.Categories.Find(model.Id);
            if (category is null)
                return NotFound();

            category.Name = model.Name;
            category.LastUpdatedOn = DateTime.Now;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
