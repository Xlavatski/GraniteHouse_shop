using GraniteHouse.Data;
using GraniteHouse.Extensions;
using GraniteHouse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GraniteHouse.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var productList = await _db.Productss.Include(m => m.ProductTypes).Include(m => m.SpecialTags).ToListAsync();

            return View(productList);
        }

        //GET
        public async Task<IActionResult> Details(int id)
        {
            var product = await _db.Productss.Include(m => m.ProductTypes).Include(m => m.SpecialTags).Where(m => m.Id == id).FirstOrDefaultAsync();

            return View(product);
        }

        ////POST
        //[HttpPost, ActionName("Details")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DetailsPOST(int id)
        //{
        //    List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
        //    if (lstShoppingCart == null) 
        //    {
        //        lstShoppingCart = new List<int>();
        //    }
        //    lstShoppingCart.Add(id);
        //    HttpContext.Session.Set("ssShoppingCart", lstShoppingCart);

        //    return RedirectToAction("Index", "Home", new { area = "Customer" });

        //    //return RedirectToAction(nameof(Index));
        //}

        //POST
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public IActionResult DetailsPOST(int id)
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if (lstShoppingCart == null) 
            {
                lstShoppingCart = new List<int>();
            }
            lstShoppingCart.Add(id);
            HttpContext.Session.Set("ssShoppingCart", lstShoppingCart);

            return RedirectToAction("Index", "Home", new { area = "Customer" });

            //return RedirectToAction(nameof(Index));
        }


        public IActionResult Remove(int id) 
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if (lstShoppingCart.Count > 0) 
            {
                if (lstShoppingCart.Contains(id)) 
                {
                    lstShoppingCart.Remove(id);
                }
            }

            HttpContext.Session.Set("ssShoppingCart", lstShoppingCart);

            return RedirectToAction(nameof(Index));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
