using System;
using Helios.DAL;
using Helios.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Helios.Controllers
{
	public class BlogInnerController:Controller
	{
        private readonly HeliosDbContext _context;

        public BlogInnerController(HeliosDbContext context)
		{
            _context = context;
        }

		public IActionResult Index(int id)
		{
			if (id == 0) return BadRequest();

			Blog? blog = _context.Blogs.FirstOrDefault(x=>x.Id == id);

			if (blog is null) return NotFound();

			return View(blog);
		}
	}
}

