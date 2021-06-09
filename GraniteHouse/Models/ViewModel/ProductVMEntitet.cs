using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraniteHouse.Models.ViewModel
{
    public class ProductVMEntitet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool Avaliable { get; set; }
        public string Image { get; set; }
        [Display(Name = "Shade Color")]
        public string ShadeColor { get; set; }
        [Display(Name = "Product Type")]
        public string ProductType { get; set; }
        [Display(Name = "Special Tag")]
        public string SpecialTag { get; set; }
    }
}
