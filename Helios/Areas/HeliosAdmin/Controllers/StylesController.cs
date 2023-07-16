using System;
using Microsoft.AspNetCore.Mvc;
using Helios.DAL;
using Helios.Entities;
using Microsoft.EntityFrameworkCore;
using Helios.ViewModel;
using Helios.Utilities.ExtensionMethods;
using System.Reflection.Metadata;
using System.Xml.Linq;
using System.ComponentModel;

namespace Helios.Areas.HeliosAdmin.Controllers
{
    [Area("HeliosAdmin")]
    public class StylesController:Controller
	{
        private readonly HeliosDbContext _context;
        private readonly IWebHostEnvironment _env;

        public StylesController(HeliosDbContext context, IWebHostEnvironment env)
		{
            _context = context;
            _env = env;
        }


        public IActionResult Index()
        {
            List<Styles> Styles = _context.Styles.Include(x => x.stylesImages).ToList();
            return View(Styles);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _context.categoryStyles.AsEnumerable();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StylesVM model)
        {
            ViewBag.Categories = _context.Categories.AsEnumerable();

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

            Styles styles = new()
            {
                shortDescription = model.shortDescription,
                textContent = model.textContent,
                URL = model.URL
            };
            string MainPath = Path.Combine(_env.WebRootPath, "assets");
            string MainPathSecond = Path.Combine(MainPath, "image");

            StylesImage falseImage = new()
            {
                IsMain = false,
                imagePath = await model.FalseImage.CreateImage(MainPathSecond, "BlogImages")
            };
            styles.stylesImages.Add(falseImage);

            StylesImage main = new()
            {
                IsMain = true,
                imagePath = await model.MainPhoto.CreateImage(MainPathSecond, "BlogImages")
            };
            styles.stylesImages.Add(main);

            foreach (int Ids in model.CategoryIds)
            {
                StylesCategory category = new()
                {
                    CategoryStyleId = Ids
                };
                styles.stylesCategories.Add(category);
            }

            _context.Styles.Add(styles);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {

            if (id == 0) return BadRequest();
            ViewBag.Categories = _context.categoryStyles.AsEnumerable();
            StylesVM? model = EditedModel(id);

            if (model is null) return BadRequest();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StylesVM edited)
        {
            ViewBag.Categories = _context.Categories.AsEnumerable();

            StylesVM? model = EditedModel(id);

            Styles? styleses = await _context.Styles.Include(x => x.stylesCategories).Include(x => x.stylesImages).FirstOrDefaultAsync(x => x.Id == id);

            if (styleses is null) return BadRequest();

            IEnumerable<string> removables = styleses.stylesImages.Where(p => !edited.ImageIds.Contains(p.Id)).Select(i => i.imagePath).AsEnumerable();
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
                await AdjustPlantPhoto(true, edited.MainPhoto, styleses);
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
                await AdjustPlantPhoto(false, edited.FalseImage, styleses);
            }

            if (edited.CategoryIds != null)
            {
                styleses.stylesCategories.RemoveAll(pt => !edited.CategoryIds.Contains(pt.CategoryStyleId));
                foreach (int categoryId in edited.CategoryIds)
                {
                    CategoryStyle category = await _context.categoryStyles.FirstOrDefaultAsync(c => c.Id == categoryId);
                    if (category is not null)
                    {
                        StylesCategory componentCategory = new() { CategoryStyle = category };
                        styleses.stylesCategories.Add(componentCategory);
                    }
                }
            }

            styleses.stylesImages.RemoveAll(p => !edited.ImageIds.Contains(p.Id));

            styleses.shortDescription = edited.shortDescription;

            styleses.textContent = edited.textContent;

            styleses.URL = edited.URL;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            StylesVM? model = EditedModel(id);

            if (model is null) return BadRequest();
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id, StylesVM deleteProduct)
        {
            if (id != deleteProduct.Id) return NotFound();
            Styles? styles = _context.Styles.FirstOrDefault(s => s.Id == id);
            if (styles is null) return NotFound();
            IEnumerable<string> removables = styles.stylesImages.Where(p => !deleteProduct.ImageIds.Contains(p.Id)).Select(i => i.imagePath)
                .AsEnumerable();
            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "image");

            foreach (string removable in removables)
            {
                string path = Path.Combine(imagefolderPath, "BlogImages", removable);
                Files.DeleteImage(path);
            }
            _context.Styles.Remove(styles);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private StylesVM? EditedModel(int id)
        {
            StylesVM? model = _context.Styles.

               Include(x => x.stylesImages).
               Include(x => x.stylesCategories).

                Select(x => new StylesVM
                {
                    Id = x.Id,

                    shortDescription = x.shortDescription,

                    textContent = x.textContent,

                    URL = x.URL,

                    CategoryIds = x.stylesCategories.Select(ac => ac.CategoryStyleId).ToList(),

                    AllImages = x.stylesImages.Select(x => new StylesImage
                    {
                        Id = x.Id,

                        IsMain = x.IsMain,

                        imagePath = x.imagePath

                    }).ToList()

                }).FirstOrDefault(x => x.Id == id);

            return model;
        }

        private async Task AdjustPlantPhoto(bool? ismain, IFormFile image, Styles Styles)
        {
            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "image");

            string filepath = Path.Combine(imagefolderPath, "BlogImages", Styles.stylesImages.FirstOrDefault(p => p.IsMain == ismain).imagePath);

            Files.DeleteImage(filepath);

            Styles.stylesImages.FirstOrDefault(p => p.IsMain == ismain).imagePath = await image.CreateImage(imagefolderPath, "BlogImages");
        }
    }

}

