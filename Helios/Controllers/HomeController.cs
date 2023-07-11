using System.Diagnostics;
using Helios.DAL;
using Helios.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Helios.Controllers;

public class HomeController : Controller
{
    private readonly HeliosDbContext _context;

    public HomeController(HeliosDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        List<Blog> Blogs = _context.Blogs.ToList();
        return View();
    }
}

