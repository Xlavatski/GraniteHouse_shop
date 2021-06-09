using GraniteHouse.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraniteHouse.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductTypes> ProductTypess { get; set; }
        public DbSet<SpecialTags> SpecialTagss { get; set; }
        public DbSet<Products> Productss { get; set; }


        public DbSet<Appointments> Appointmentss { get; set; }
        public DbSet<ProductSelectedForAppointment> ProductSelectedForAppointmentss { get; set; }

        public DbSet<ApplicationUser> ApplicationUserss { get; set; }
    }
}
