using System;
using Microsoft.AspNetCore.Mvc;
using Helios.DAL;
using Helios.Entities;
using Microsoft.EntityFrameworkCore;
using Helios.ViewModel;
using Helios.Utilities.ExtensionMethods;

namespace Helios.Areas.HeliosAdmin.Controllers
{
    [Area("HeliosAdmin")]
    public class IntroductionCardController:Controller
	{
        private readonly HeliosDbContext _context;

        private readonly IWebHostEnvironment _env;

        public IntroductionCardController(HeliosDbContext context, IWebHostEnvironment env)
		{
            _context = context;

            _env = env;
        }

        public IActionResult Index()
        {
            List<IntroductionCards> introductionCards = _context.introductionCards.ToList();

            return View(introductionCards);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IntroductionCardVM category)
        {
            if (!ModelState.IsValid)
            {

                foreach (string message in ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))
                {
                    ModelState.AddModelError("", message);
                }

                return View();
            }

            bool isDuplicated = _context.Categories.Any(c => c.Name == category.CardInsideTitle);
            if (isDuplicated)
            {
                ModelState.AddModelError("", "You cannot duplicate value");
                return View();
            }
            IntroductionCards cat = new()
            {
                CardInsideTitle = category.CardInsideTitle,

                CardInsideText = category.CardInsideText
            };

            _context.introductionCards.Add(cat);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();

            IntroductionCards? category = _context.introductionCards.FirstOrDefault(c => c.Id == id);

            _context.Remove(category);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}

