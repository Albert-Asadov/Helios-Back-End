using System;
using Helios.DAL;
using Helios.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Helios.Controllers
{
	public class StylesController:Controller
	{
        private readonly HeliosDbContext _context;

        public StylesController(HeliosDbContext context)
		{
            _context = context;
        }


        public IActionResult Index()
        {
            ViewBag.CategoryStyles = _context.categoryStyles.ToList();

            var firstCategory = _context.categoryStyles.FirstOrDefault();

            if (firstCategory != null)
            {
                return RedirectToAction("LoadContent", new { categoryId = firstCategory.Id });
            }
            return View();
        }

        public IActionResult LoadContent(int categoryId)
        {
            ViewBag.CategoryStyles = _context.categoryStyles.ToList();

            ViewBag.categoryId = categoryId;

            Styles? styles = _context.Styles

                    .Include(x => x.stylesImages)
                                     .Include(x => x.stylesCategories)
                                         .ThenInclude(x => x.CategoryStyle)
                                            .FirstOrDefault(c => c.stylesCategories.Any(cc => cc.CategoryStyle.Id == categoryId));


            if (styles == null)
            {
                return NotFound();
            }

            return View(styles);

        }
    }
}

