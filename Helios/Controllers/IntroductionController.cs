using System;
using Helios.DAL;
using Helios.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Helios.Controllers
{
	public class IntroductionController:Controller
	{
        private readonly HeliosDbContext _context;

        public IntroductionController(HeliosDbContext context)
		{
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Cards = _context.introductionCards.ToList();
            List<Introduction>? introduction = _context.Introductions.Include(x=>x.introductionImages).ToList();

            return View(introduction);
        }
	}
}

