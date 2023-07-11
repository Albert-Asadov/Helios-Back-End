using System;
using Helios.DAL;
using Helios.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Helios.Controllers
{
	public class BlogController:Controller
	{
        private readonly HeliosDbContext _context;

        public BlogController(HeliosDbContext context)
		{
            _context = context;
        }

		public IActionResult Index()
		{
			List<Blog> blogs = _context.Blogs.ToList();
			return View(blogs);
		}
	}
}

