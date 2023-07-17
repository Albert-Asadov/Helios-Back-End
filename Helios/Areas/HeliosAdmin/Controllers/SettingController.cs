using System;
using Microsoft.AspNetCore.Mvc;
using Helios.DAL;
using Helios.Entities;
using Microsoft.EntityFrameworkCore;
using Helios.ViewModel;


namespace Helios.Areas.HeliosAdmin.Controllers
{
    [Area("HeliosAdmin")]
    public class SettingController:Controller
	{
        private readonly HeliosDbContext _context;

        public SettingController(HeliosDbContext context)
		{
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Setting> settings = _context.Settings.AsEnumerable();

            return View(settings);
        }


        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Setting CreatedSetting)
        {
            Setting setting = new()
            {
                Key = CreatedSetting.Key,
                Value = CreatedSetting.Value
            };

            _context.Settings.Add(setting);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();

            Setting setting = _context.Settings.FirstOrDefault(t => t.Id == id);

            if (setting is null) return BadRequest();

            return View(setting);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Setting EditedSetting)
        {
            if (id != EditedSetting.Id) return BadRequest();
            Setting setting = _context.Settings.FirstOrDefault(t => t.Id == id);
            if (setting is null) return NotFound();
            bool duplicate = _context.Settings.Any(c => c.Key == EditedSetting.Key && c.Value == EditedSetting.Value);


            if (duplicate)
            {
                ModelState.AddModelError("", "the Tag is actually have");
                return View();
            }


            setting.Key = EditedSetting.Key;
            setting.Value = EditedSetting.Value;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();

            Setting setting = _context.Settings.FirstOrDefault(t => t.Id == id);

            _context.Remove(setting);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}

