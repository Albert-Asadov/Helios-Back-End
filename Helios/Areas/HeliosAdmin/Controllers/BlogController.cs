using System;
using Helios.DAL;
using Helios.Entities;
using Helios.Utilities.ExtensionMethods;
using Helios.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Helios.Areas.HeliosAdmin.Controllers
{
    [Area("HeliosAdmin")]
    public class BlogController : Controller
    {
        private readonly HeliosDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogController(HeliosDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public IActionResult Index()
        {
            IEnumerable<Blog> blog = _context.Blogs.OrderByDescending(x=>x.Id).Include(x => x.blogImages).AsEnumerable();

            return View(blog);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogVM model)
        {

            TempData["InvalidImages"] = string.Empty;


            if (!model.MainPhoto.IsValidFile("image/"))
            {
                ModelState.AddModelError(string.Empty, "Please choose image file");
                return View();
            }
            if (!model.MainPhoto.IsValidLength(3))
            {
                ModelState.AddModelError(string.Empty, "Maximum 3MB");
                return View();
            }
            string MainPath = Path.Combine(_env.WebRootPath, "assets");
            string MainPathSecond = Path.Combine(MainPath, "image");



            Blog blog = new()
            {
                CardTitleDetail = model.CardTitleDetail,
                TitleCard = model.TitleCard,
                ShortDescription = model.ShortDescription,
                TextContent = model.TextContent,

                 DesignFilter = model.DesignFilter
            };

            BlogImages main = new()
            {
                IsMain = true,
                ImagePath = await model.MainPhoto.CreateImage(MainPathSecond, "BlogImages")
            };

            blog.blogImages.Add(main);

            _context.Blogs.Add(blog);

            _context.SaveChanges();

            return RedirectToAction("Index", "Blog");
        }



        public IActionResult Edit(int id)
        {
            if (id == 0) return BadRequest();
            BlogVM? model = EditedModel(id);

            if (model is null) return BadRequest();


            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogVM edited)
        {
            BlogVM? model = EditedModel(id);

            Blog? blog = _context.Blogs.Include(x => x.blogImages).FirstOrDefault(x => x.Id == id);

            if (blog is null) return BadRequest();

            IEnumerable<string> removables = blog.blogImages.Where(p => !edited.ImageIds.Contains(p.Id)).Select(i => i.ImagePath).AsEnumerable();
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
                await AdjustPlantPhoto(true, edited.MainPhoto, blog);
            }

            blog.blogImages.RemoveAll(p => !edited.ImageIds.Contains(p.Id));

            blog.TitleCard = edited.TitleCard;

            blog.CardTitleDetail = edited.CardTitleDetail;

            blog.ShortDescription = edited.ShortDescription;

            blog.TextContent = edited.TextContent;

            blog.DesignFilter = edited.DesignFilter;

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            BlogVM? model = EditedModel(id);

            if (model is null) return BadRequest();
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id, BlogVM deleteProduct)
        {
            if (id != deleteProduct.Id) return NotFound();
            Blog? blog = _context.Blogs.FirstOrDefault(s => s.Id == id);
            if (blog is null) return NotFound();
            IEnumerable<string> removables = blog.blogImages.Where(p => !deleteProduct.ImageIds.Contains(p.Id)).Select(i => i.ImagePath)
                .AsEnumerable();
            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "image");

            foreach (string removable in removables)
            {
                string path = Path.Combine(imagefolderPath, "BlogImages", removable);
                Files.DeleteImage(path);
            }
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }



        private BlogVM? EditedModel(int id)
            {
            BlogVM? model = _context.Blogs.

               Include(x => x.blogImages).

                Select(x => new BlogVM
                {
                    Id = x.Id,

                    TitleCard = x.TitleCard,

                    CardTitleDetail = x.CardTitleDetail,

                    ShortDescription = x.ShortDescription,

                    TextContent = x.TextContent,

                    DesignFilter = x.DesignFilter,

                    AllImages = x.blogImages.Select(p => new BlogImages
                    {
                        Id = p.Id,

                        IsMain = p.IsMain,

                        ImagePath = p.ImagePath

                    }).ToList()

                }).FirstOrDefault(x => x.Id == id);

            return model;
            }

        private async Task AdjustPlantPhoto(bool? ismain, IFormFile image, Blog blog)
        {
            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "image");

            string filepath = Path.Combine(imagefolderPath, "BlogImages", blog.blogImages.FirstOrDefault(p => p.IsMain == ismain).ImagePath);

            Files.DeleteImage(filepath);

            blog.blogImages.FirstOrDefault(p => p.IsMain == ismain).ImagePath = await image.CreateImage(imagefolderPath, "BlogImages");
        }
    }

}

