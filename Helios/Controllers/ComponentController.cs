using System;
using Helios.DAL;
using Helios.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Helios.Controllers
{
	public class ComponentController:Controller
	{
        private readonly HeliosDbContext _context;

        public ComponentController(HeliosDbContext context)
		{
            _context = context;
        }


        public IActionResult Index(int categoryId)
        {
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.categoryId = categoryId;
            Component? component = _context.Components
                    .Include(x=>x.ComponentImages)
                                     .Include(x => x.componentCategories)
                                         .ThenInclude(x => x.Category)
                                            .FirstOrDefault(c => c.componentCategories.Any(cc => cc.Category.Id == categoryId));


            if (component == null)
            {
                return NotFound();
            }

            return View(component);

        }



    }
}

