using System;
using Microsoft.AspNetCore.Mvc;
using Helios.DAL;
using Helios.Entities;
using Microsoft.EntityFrameworkCore;
using Helios.ViewModel;

namespace Helios.Areas.HeliosAdmin.Controllers
{
    [Area("HeliosAdmin")]
    public class CategoryController:Controller
	{
        private readonly HeliosDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryController(HeliosDbContext context, IWebHostEnvironment env)
		{
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            List<Category> category = _context.Categories.ToList();
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryVM category)
        {
            if (!ModelState.IsValid)
            {

                foreach (string message in ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))
                {
                    ModelState.AddModelError("", message);
                }

                return View();
            }

            bool isDuplicated = _context.Categories.Any(c => c.Name == category.Name);
            if (isDuplicated)
            {
                ModelState.AddModelError("", "You cannot duplicate value");
                return View();
            }
            Category cat = new()
            {
                Name = category.Name
            };
            _context.Categories.Add(cat);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();

            Category? category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category is null)
            {
                return BadRequest();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Category EditedCategory)
        {
            if (id == 0) return NotFound();
            Category? category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category is null)
            {
                return BadRequest();
            }

            bool ContainsName = _context.Categories.Any(c => c.Name == EditedCategory.Name);

            if (ContainsName == true)
            {
                ModelState.AddModelError("", "You Have This category Name");

                return View();
            }

            category.Name = EditedCategory.Name;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();

            Category? category = _context.Categories.FirstOrDefault(c => c.Id == id);

            _context.Remove(category);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}

