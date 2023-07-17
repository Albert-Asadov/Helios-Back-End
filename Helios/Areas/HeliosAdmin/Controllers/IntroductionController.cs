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
    public class IntroductionController:Controller
	{
        private readonly HeliosDbContext _context;

        private readonly IWebHostEnvironment _env;

        public IntroductionController(HeliosDbContext context, IWebHostEnvironment env)
        {
            _context = context;

            _env = env;
        }

        public IActionResult Index()
        {
            List<Introduction>? introduction = _context.Introductions.Include(x=>x.introductionImages).ToList();

            return View(introduction);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IntroductionVM model)
        {
            if (!model.MainPhoto.IsValidFile("image/"))
            {
                ModelState.AddModelError(string.Empty, "Please choose image file");

                return View();
            }
            if (!model.MainPhoto.IsValidLength(1))
            {
                ModelState.AddModelError(string.Empty, "Please choose image which size is maximum 1MB");

                return View();
            }

            Introduction intro = new()
            {
                RightSideTitle = model.RightSideTitle,

                LeftSideTitle = model.LeftSideTitle,
            };

            string MainPath = Path.Combine(_env.WebRootPath, "assets");

            string MainPathSecond = Path.Combine(MainPath, "image");

            IntroductionImage falseImage = new()
            {
                IsMain = false,

                imagePath = await model.FalseImage.CreateImage(MainPathSecond, "BlogImages")
            };
            intro.introductionImages.Add(falseImage);

            IntroductionImage main = new()
            {
                IsMain = true,

                imagePath = await model.MainPhoto.CreateImage(MainPathSecond, "BlogImages")
            };
            intro.introductionImages.Add(main);

            _context.Introductions.Add(intro);

            _context.SaveChanges();

            return RedirectToAction("Index", "Introduction");
        }

        public IActionResult Edit(int id)
        {

            if (id == 0) return BadRequest();
            ViewBag.Categories = _context.Categories.AsEnumerable();
            IntroductionVM? model = EditedModel(id);

            if (model is null) return BadRequest();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IntroductionVM edited)
        {
            ViewBag.Categories = _context.Categories.AsEnumerable();

            IntroductionVM? model = EditedModel(id);

            Introduction? intro = await _context.Introductions.Include(x => x.introductionImages).FirstOrDefaultAsync(x => x.Id == id);

            if (intro is null) return BadRequest();

            IEnumerable<string> removables = intro.introductionImages.Where(p => !edited.ImageIds.Contains(p.Id)).Select(i => i.imagePath).AsEnumerable();

            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "image");

            foreach (string removable in removables)
            {
                string path = Path.Combine(imagefolderPath, "BlogImages", removable);
                Files.DeleteImage(path);
            }

            if (edited.MainPhoto is not null)
            {
                if (!edited.MainPhoto.IsValidFile("image/"))
                {
                    ModelState.AddModelError(string.Empty, "Please choose image file");
                    return View();
                }
                if (!edited.MainPhoto.IsValidLength(2))
                {
                    ModelState.AddModelError(string.Empty, "Please choose image which size is maximum 2MB");
                    return View();
                }
                await AdjustPlantPhoto(true, edited.MainPhoto, intro);
            }

            if (edited.FalseImage is not null)
            {
                if (!edited.FalseImage.IsValidFile("image/"))
                {
                    ModelState.AddModelError(string.Empty, "Please choose image file");
                    return View();
                }
                if (!edited.FalseImage.IsValidLength(2))
                {
                    ModelState.AddModelError(string.Empty, "Please choose image which size is maximum 2MB");
                    return View();
                }
                await AdjustPlantPhoto(false, edited.FalseImage, intro);
            }


            intro.introductionImages.RemoveAll(p => !edited.ImageIds.Contains(p.Id));

            intro.LeftSideTitle = edited.LeftSideTitle;

            intro.RightSideTitle = edited.RightSideTitle;


            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            IntroductionVM? model = EditedModel(id);

            if (model is null) return BadRequest();
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id, IntroductionVM deleteProduct)
        {
            if (id != deleteProduct.Id) return NotFound();

            Introduction? comp = _context.Introductions.FirstOrDefault(s => s.Id == id);

            if (comp is null) return NotFound();

            IEnumerable<string> removables = comp.introductionImages.Where(p => !deleteProduct.ImageIds.Contains(p.Id)).Select(i => i.imagePath)
                .AsEnumerable();
            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "image");

            foreach (string removable in removables)
            {
                string path = Path.Combine(imagefolderPath, "BlogImages", removable);
                Files.DeleteImage(path);
            }
            _context.Introductions.Remove(comp);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private IntroductionVM? EditedModel(int id)
        {
            IntroductionVM? model = _context.Introductions.

               Include(x => x.introductionImages).

                Select(x => new IntroductionVM
                {
                    Id = x.Id,

                    RightSideTitle = x.RightSideTitle,

                    LeftSideTitle = x.LeftSideTitle,

                    AllImages = x.introductionImages.Select(x => new IntroductionImage
                    {
                        Id = x.Id,

                        IsMain = x.IsMain,

                        imagePath = x.imagePath

                    }).ToList()

                }).FirstOrDefault(x => x.Id == id);

            return model;
        }

        private async Task AdjustPlantPhoto(bool? ismain, IFormFile image, Introduction intro)
        {
            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "image");

            string filepath = Path.Combine(imagefolderPath, "BlogImages", intro.introductionImages.FirstOrDefault(p => p.IsMain == ismain).imagePath);

            Files.DeleteImage(filepath);

            intro.introductionImages.FirstOrDefault(p => p.IsMain == ismain).imagePath = await image.CreateImage(imagefolderPath, "BlogImages");
        }
    }
}

