using System.Diagnostics;
using Helios.DAL;
using Helios.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        ViewBag.New = _context.Blogs.OrderByDescending(x => x.Id).Include(x=>x.blogImages).Take(3).ToList() ;
        ViewBag.Design = _context.Blogs.OrderByDescending(x => x.Id).Include(x => x.blogImages).Take(6).ToList();
        return View();
    }
}

