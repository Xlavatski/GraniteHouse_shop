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
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var result = _db.ProductTypess.ToList();
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
        public async Task<IActionResult> Create(ProductTypes productTypes) 
        {
            if (ModelState.IsValid) 
            {
                _db.Add(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }

        //GET Edit Action Method

        public async Task<IActionResult> Edit(int? id) 
        {
            if (id == null) 
            {
                return NotFound();
            }

            var productType = await _db.ProductTypess.FindAsync(id);
            if (productType == null) 
            {
                return NotFound();
            }

            return View(productType);
        }

        //POST Edit Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductTypes productTypes) 
        {
            if (id != productTypes.Id) 
            {
                return NotFound();
            }

            if (ModelState.IsValid) 
            {
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }

        //GET Details Action Method
        public async Task<IActionResult> Details(int? id) 
        {
            if (id == null) 
            {
                return NotFound();
            }

            var productType =await _db.ProductTypess.FindAsync(id);
            if (productType == null) 
            {
                return NotFound();
            }

            return View(productType);
        }


        //GET Delete Action Method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) 
            {
                return NotFound();
            }

            var productType = await _db.ProductTypess.FindAsync(id);
            if (productType == null) 
            {
                return NotFound();
            }

            return View(productType);
        }

        //POST Delete Action Method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) 
        {
            var productType = await _db.ProductTypess.FindAsync(id);
            _db.ProductTypess.Remove(productType);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
