using GraniteHouse.Data;
using GraniteHouse.Extensions;
using GraniteHouse.Models;
using GraniteHouse.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GraniteHouse.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ShoppingCartViewModel ShoppingCartVM { get; set; }

        public ShoppingCartController(ApplicationDbContext db)
        {
            _db = db;
            ShoppingCartVM = new ShoppingCartViewModel()
            {
                Products = new List<Models.Products>()
                //Products = _db.Productss.ToList()
            };
        }

        //GET Index shopping cart
        public IActionResult Index()
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            if (lstShoppingCart != null) 
            {
                if (lstShoppingCart.Count > 0)
                {
                    foreach (int cartItem in lstShoppingCart)
                    {
                        Products prod = _db.Productss.Include(p => p.SpecialTags).Include(p => p.ProductTypes).Where(p => p.Id == cartItem).FirstOrDefault();
                        ShoppingCartVM.Products.Add(prod);
                    }
                }
            }

            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        //POST Index shopping cart
        public IActionResult IndexPOST() 
        {
            List<int> lstCartItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");
         
            ShoppingCartVM.Appointments.AppointmentDate = ShoppingCartVM.Appointments.AppointmentDate
                                                            .AddHours(ShoppingCartVM.Appointments.AppointmentTime.Hour)
                                                            .AddMinutes(ShoppingCartVM.Appointments.AppointmentTime.Minute);

            Appointments appointments = ShoppingCartVM.Appointments;
            _db.Appointmentss.Add(appointments);
            _db.SaveChanges();

            int appointmentId = appointments.Id;

            foreach (int productId in lstCartItems) 
            {
                ProductSelectedForAppointment productSelectedForAppointment = new ProductSelectedForAppointment()
                {
                    AppointmentId = appointmentId,
                    ProductId = productId
                };

                _db.ProductSelectedForAppointmentss.Add(productSelectedForAppointment);
            }
            _db.SaveChanges();
            lstCartItems = new List<int>();
            HttpContext.Session.Set("ssShoppingCart", lstCartItems);

            return RedirectToAction("AppointmentConfirmation", "ShoppingCart", new { Id = appointmentId });
        }

        public IActionResult Remove(int id) 
        {
            List<int> lstCartItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            if (lstCartItems.Count > 0) 
            {
                if (lstCartItems.Contains(id)) 
                {
                    lstCartItems.Remove(id);
                }
            }

            HttpContext.Session.Set("ssShoppingCart", lstCartItems);

            return RedirectToAction(nameof(Index));
        }


        //GET action method
        public IActionResult AppointmentConfirmation(int id) 
        {
            ShoppingCartVM.Appointments = _db.Appointmentss.Where(a => a.Id == id).FirstOrDefault();

            List<ProductSelectedForAppointment> objProductList = _db.ProductSelectedForAppointmentss.Where(p => p.AppointmentId == id).ToList();

            foreach (ProductSelectedForAppointment prodAptObj in objProductList) 
            {
                ShoppingCartVM.Products.Add(_db.Productss.Include(p => p.ProductTypes).Include(p => p.SpecialTags).Where(p => p.Id == prodAptObj.ProductId).FirstOrDefault());
            }

            return View(ShoppingCartVM);
        }
    }
}
