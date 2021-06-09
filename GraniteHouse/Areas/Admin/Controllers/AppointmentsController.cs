using GraniteHouse.Data;
using GraniteHouse.Models;
using GraniteHouse.Models.ViewModel;
using GraniteHouse.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GraniteHouse.Areas.Admin.Controllers
{
    [Authorize(Roles =SD.SuperAdminEndUser + "," + SD.AdminEndUser)]
    [Area("Admin")]
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private int PageSize = 3;

        public AppointmentsController(ApplicationDbContext db)
        {
            _db = db;
        }



        public IActionResult Index(int productPage = 1, string searchName = null, string searchEmail = null, string searchPhone = null, string searchDate = null)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            AppointmentViewModel appointmentVM = new AppointmentViewModel()
            {
                Appointments = new List<Models.Appointments>()
            };

            StringBuilder param = new StringBuilder();

            param.Append("/Admin/Appointments?productPage=:");
            param.Append("&searchName=");

            if (searchName != null)
            {
                param.Append(searchName);
            }

            param.Append("&searchEmail=");

            if (searchEmail != null)
            {
                param.Append(searchEmail);
            }

            param.Append("&searchPhone=");

            if (searchPhone != null)
            {
                param.Append(searchPhone);
            }

            param.Append("&searchDate=");

            if (searchDate != null)
            {
                param.Append(searchDate);
            }

            appointmentVM.Appointments = _db.Appointmentss.Include(a => a.SalesPerson).ToList();
            if (User.IsInRole(SD.AdminEndUser))
            {
                appointmentVM.Appointments = appointmentVM.Appointments.Where(a => a.SalesPersonId == claim.Value).ToList();
            }


            if (searchName != null)
            {
                appointmentVM.Appointments = appointmentVM.Appointments.Where(a => a.CustomerName.ToLower().Contains(searchName.ToLower())).ToList();
            }
            if (searchEmail != null)
            {
                appointmentVM.Appointments = appointmentVM.Appointments.Where(a => a.CustomerEmail.ToLower().Contains(searchEmail.ToLower())).ToList();
            }
            if (searchPhone != null)
            {
                appointmentVM.Appointments = appointmentVM.Appointments.Where(a => a.CustomerPhoneNumber.ToLower().Contains(searchPhone.ToLower())).ToList();
            }

            if (searchDate != null)
            {
                try
                {
                    DateTime appDate = Convert.ToDateTime(searchDate);
                    appointmentVM.Appointments = appointmentVM.Appointments.Where(a => a.AppointmentDate.ToShortDateString().Equals(appDate.ToShortDateString())).ToList();
                }
                catch (Exception ex)
                {

                }
            }

            var count = appointmentVM.Appointments.Count;

            appointmentVM.Appointments = appointmentVM.Appointments.OrderBy(p => p.AppointmentDate)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize).ToList();

            appointmentVM.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = 2,
                TotalItems = count,
                urlParm = param.ToString()
            };

            return View(appointmentVM);
        }



        //GET Edit
        public IActionResult Edit(int? id) 
        {
            if (id == null) 
            {
                return NotFound();
            }

            var productList = (IEnumerable<Products>)(from p in _db.Productss
                                                      join a in _db.ProductSelectedForAppointmentss
                                                      on p.Id equals a.ProductId
                                                      where a.AppointmentId == id
                                                      select p).Include("ProductTypes");

            //no used
            var productListSecond = _db.ProductSelectedForAppointmentss.Where(i => i.AppointmentId == id)
                                            .Include(i => i.Products)
                                            .Select(i => i.Products)
                                            .ToList();


            AppointmentsDetailsViewModel objAppointmentVM = new AppointmentsDetailsViewModel()
            {
                Appointment = _db.Appointmentss.Include(a => a.SalesPerson).Where(a => a.Id == id).FirstOrDefault(),
                SalesPerson = _db.ApplicationUserss.ToList(),
                Products = productList.ToList()
                //Products = productListSecond
            };

            return View(objAppointmentVM);
        }

        //POST Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AppointmentsDetailsViewModel objAppointmentVM) 
        {
            if (ModelState.IsValid) 
            {
                objAppointmentVM.Appointment.AppointmentDate = objAppointmentVM.Appointment.AppointmentDate
                                    .AddHours(objAppointmentVM.Appointment.AppointmentTime.Hour)
                                    .AddMinutes(objAppointmentVM.Appointment.AppointmentTime.Minute);

                var appoitmentFromDb = _db.Appointmentss.Where(a => a.Id == objAppointmentVM.Appointment.Id).FirstOrDefault();

                appoitmentFromDb.CustomerName = objAppointmentVM.Appointment.CustomerName;
                appoitmentFromDb.CustomerEmail = objAppointmentVM.Appointment.CustomerEmail;
                appoitmentFromDb.CustomerPhoneNumber = objAppointmentVM.Appointment.CustomerPhoneNumber;
                appoitmentFromDb.AppointmentDate = objAppointmentVM.Appointment.AppointmentDate;
                appoitmentFromDb.isConfirmed = objAppointmentVM.Appointment.isConfirmed;
                if (User.IsInRole(SD.SuperAdminEndUser)) 
                {
                    appoitmentFromDb.SalesPersonId = objAppointmentVM.Appointment.SalesPersonId;
                }

                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(objAppointmentVM);
        }

        //GET Details
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productList = (IEnumerable<Products>)(from p in _db.Productss
                                                      join a in _db.ProductSelectedForAppointmentss
                                                      on p.Id equals a.ProductId
                                                      where a.AppointmentId == id
                                                      select p).Include("ProductTypes");

            AppointmentsDetailsViewModel objAppointmentVM = new AppointmentsDetailsViewModel()
            {
                Appointment = _db.Appointmentss.Include(a => a.SalesPerson).Where(a => a.Id == id).FirstOrDefault(),
                SalesPerson = _db.ApplicationUserss.ToList(),
                Products = productList.ToList()
            };

            return View(objAppointmentVM);
        }

        //GET Delete
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productList = (IEnumerable<Products>)(from p in _db.Productss
                                                      join a in _db.ProductSelectedForAppointmentss
                                                      on p.Id equals a.ProductId
                                                      where a.AppointmentId == id
                                                      select p).Include("ProductTypes");

            AppointmentsDetailsViewModel objAppointmentVM = new AppointmentsDetailsViewModel()
            {
                Appointment = _db.Appointmentss.Include(a => a.SalesPerson).Where(a => a.Id == id).FirstOrDefault(),
                SalesPerson = _db.ApplicationUserss.ToList(),
                Products = productList.ToList()
            };

            return View(objAppointmentVM);
        }

        //POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) 
        {
            var appointment = await _db.Appointmentss.FindAsync(id);
            _db.Appointmentss.Remove(appointment);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
