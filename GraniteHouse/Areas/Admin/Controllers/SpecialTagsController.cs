using GraniteHouse.Data;
using GraniteHouse.Models;
using GraniteHouse.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraniteHouse.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.SuperAdminEndUser)]
    [Area("Admin")]
    public class SpecialTagsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SpecialTagsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var result = _db.SpecialTagss.ToList();
            return View(result);
        }

        //GET Create Action Method
        public IActionResult Create()
        {
            return View();
        }

        //POST Create Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTags specialTags)
        {
            if (ModelState.IsValid)
            {
                _db.Add(specialTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTags);
        }

        //GET Edit Action Method 
        public async Task<IActionResult> Edit(int? id) 
        {
            if (id == null) 
            {
                return NotFound();
            }

            var specialTag = await _db.SpecialTagss.FindAsync(id);
            if (specialTag == null) 
            {
                return NotFound();
            }

            return View(specialTag);
        }

        //POST Edit Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SpecialTags specialTags) 
        {
            if (id != specialTags.Id) 
            {
                return NotFound();
            }

            if (ModelState.IsValid) 
            {
                _db.Update(specialTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTags);
        }

        //GET Details Action Method
        public async Task<IActionResult> Details(int? id) 
        {
            if (id == null) 
            {
                return NotFound();
            }

            var specialTag = await _db.SpecialTagss.FindAsync(id);
            if (specialTag == null) 
            {
                return NotFound();
            }

            return View(specialTag);
        }

        //GET Delete Action Method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialTag = await _db.SpecialTagss.FindAsync(id);
            if (specialTag == null)
            {
                return NotFound();
            }

            return View(specialTag);
        }

        //POST Delete Action Method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialTag = await _db.SpecialTagss.FindAsync(id);
            _db.SpecialTagss.Remove(specialTag);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
