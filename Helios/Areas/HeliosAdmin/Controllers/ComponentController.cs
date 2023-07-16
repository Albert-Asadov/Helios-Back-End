using System;
using Microsoft.AspNetCore.Mvc;
using Helios.DAL;
using Helios.Entities;
using Microsoft.EntityFrameworkCore;
using Helios.ViewModel;
using Helios.Utilities.ExtensionMethods;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace Helios.Areas.HeliosAdmin.Controllers
{
    [Area("HeliosAdmin")]
    public class ComponentController : Controller
    {
        private readonly HeliosDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ComponentController(HeliosDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            List<Component> comp = _context.Components.Include(x => x.ComponentImages).ToList();
            return View(comp);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.AsEnumerable();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComponentVM model)
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

            Component component = new()
            {
                shortDescription = model.shortDescription,
                textContent = model.textContent,
                URL = model.URL
            };
            string MainPath = Path.Combine(_env.WebRootPath, "assets");
            string MainPathSecond = Path.Combine(MainPath, "image");

            ComponentImage falseImage = new()
            {
                IsMain = false,
                imagePath = await model.FalseImage.CreateImage(MainPathSecond, "BlogImages")
            };
            component.ComponentImages.Add(falseImage);

            ComponentImage main = new()
            {
                IsMain = true,
                imagePath = await model.MainPhoto.CreateImage(MainPathSecond, "BlogImages")
            };
            component.ComponentImages.Add(main);

            foreach (int Ids in model.CategoryIds)
            {
                ComponentCategory category = new()
                {
                   CategoryId = Ids
                };
                component.componentCategories.Add(category);
            }

            _context.Components.Add(component);

            _context.SaveChanges();

            return RedirectToAction("Index", "Component");
        }

        public IActionResult Edit(int id)
        {

            if (id == 0) return BadRequest();
            ViewBag.Categories = _context.Categories.AsEnumerable();
            ComponentVM? model = EditedModel(id);

            if (model is null) return BadRequest();

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ComponentVM edited)
        {
            ViewBag.Categories = _context.Categories.AsEnumerable();

            ComponentVM? model = EditedModel(id);

            Component? component = await _context.Components.Include(x => x.componentCategories).Include(x => x.ComponentImages).FirstOrDefaultAsync(x => x.Id == id);

            if (component is null) return BadRequest();

            IEnumerable<string> removables = component.ComponentImages.Where(p => !edited.ImageIds.Contains(p.Id)).Select(i => i.imagePath).AsEnumerable();
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
                await AdjustPlantPhoto(true, edited.MainPhoto, component);
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
                await AdjustPlantPhoto(false, edited.FalseImage, component);
            }

            if (edited.CategoryIds != null)
            {
                component.componentCategories.RemoveAll(pt => !edited.CategoryIds.Contains(pt.CategoryId));
                foreach (int categoryId in edited.CategoryIds)
                {
                    Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
                    if (category is not null)
                    {
                        ComponentCategory componentCategory = new() { Category = category };
                        component.componentCategories.Add(componentCategory);
                    }
                }
            }

            component.ComponentImages.RemoveAll(p => !edited.ImageIds.Contains(p.Id));

            component.shortDescription = edited.shortDescription;

            component.textContent = edited.textContent;

            component.URL = edited.URL;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


            private ComponentVM? EditedModel(int id)
        {
            ComponentVM? model = _context.Components.

               Include(x => x.ComponentImages).
               Include(x=>x.componentCategories).

                Select(x => new ComponentVM
                {
                    Id = x.Id,

                    shortDescription = x.shortDescription,

                    textContent = x.textContent,

                    URL = x.URL,

                    CategoryIds = x.componentCategories.Select(ac => ac.CategoryId).ToList(),

                    AllImages = x.ComponentImages.Select(x => new ComponentImage
                     {
                         Id = x.Id,

                         IsMain = x.IsMain,

                         imagePath = x.imagePath

                     }).ToList()

                }).FirstOrDefault(x => x.Id == id);

            return model;
        }

        private async Task AdjustPlantPhoto(bool? ismain, IFormFile image, Component component)
        {
            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "image");

            string filepath = Path.Combine(imagefolderPath, "BlogImages", component.ComponentImages.FirstOrDefault(p => p.IsMain == ismain).imagePath);

            Files.DeleteImage(filepath);

            component.ComponentImages.FirstOrDefault(p => p.IsMain == ismain).imagePath = await image.CreateImage(imagefolderPath, "BlogImages");
        }
    }
}


