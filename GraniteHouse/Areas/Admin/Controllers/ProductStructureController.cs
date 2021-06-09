using GraniteHouse.Data;
using GraniteHouse.Models;
using GraniteHouse.Models.Queries;
using GraniteHouse.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace GraniteHouse.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.SuperAdminEndUser + "," + SD.AdminEndUser)]
    [Area("Admin")]
    public class ProductStructureController : Controller
    {
        private readonly ApplicationDbContext _db;


        public ProductStructureController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {

            var result = _db.Productss.Include(m => m.ProductTypes).ToList().GroupBy(i => i.ProductTypes).Select(i => new GroupByModelQuery
            {
                ProductTypes = i.Key,
                Count = i.Count()
            });

            return View(result);
        }
    }
}
