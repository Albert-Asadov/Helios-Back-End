using System;
using Helios.DAL;
using Helios.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Helios.Controllers
{
	public class BlogController:Controller
	{
        private readonly HeliosDbContext _context;

        public BlogController(HeliosDbContext context)
		{
            _context = context;
        }

		public IActionResult Index(int page = 1)
		{
            ViewBag.TotalPage = Math.Ceiling((double)_context.Blogs.Count() / 9);
			ViewBag.CurrentPage = page;
            List<Blog> blogs = _context.Blogs.OrderByDescending(x => x.Id).Include(x=>x.blogImages).AsNoTracking().Skip((page - 1) * 9).Take(9).ToList();
			return View(blogs);
		}
	}
}

