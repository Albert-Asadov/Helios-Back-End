using System;
using Microsoft.AspNetCore.Mvc;
using Helios.DAL;
using Helios.Entities;
using Microsoft.EntityFrameworkCore;
using Helios.ViewModel;

namespace Helios.Areas.HeliosAdmin.Controllers
{
    [Area("HeliosAdmin")]
    public class CategoryStylesController:Controller
	{
        private readonly HeliosDbContext _context;

        public CategoryStylesController(HeliosDbContext context)
		{
            _context = context;
        }

        public IActionResult Index()
        {
            List<CategoryStyle> categoryStyle = _context.categoryStyles.ToList();

            return View(categoryStyle);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryStyleVM category)
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
            CategoryStyle catStyle = new()
            {
                Name = category.Name
            };
            _context.categoryStyles.Add(catStyle);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();

            CategoryStyle? categoryStyles = _context.categoryStyles.FirstOrDefault(c => c.Id == id);

            if (categoryStyles is null)
            {
                return BadRequest();
            }
            return View(categoryStyles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CategoryStyle EditedCategory)
        {
            if (id == 0) return NotFound();
            CategoryStyle? category = _context.categoryStyles.FirstOrDefault(c => c.Id == id);

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

            CategoryStyle? category = _context.categoryStyles.FirstOrDefault(c => c.Id == id);

            _context.Remove(category);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}

