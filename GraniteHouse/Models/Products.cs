using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GraniteHouse.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool Avaliable { get; set; }
        public string Image { get; set; }
        [Display(Name ="Shade Color")]
        public string ShadeColor { get; set; }

        [Display(Name ="Product Type")]
        public int ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")]
        [Display(Name = "Product Type")]
        public virtual ProductTypes ProductTypes { get; set; }

        [Display(Name = "Special Tag")]
        public int SpecialTagId { get; set; }

        [ForeignKey("SpecialTagId")]
        [Display(Name = "Special Tag")]
        public virtual SpecialTags SpecialTags { get; set; }
    }
}
