using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraniteHouse.Models
{
    public class SpecialTags
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string SpecialName { get; set; }
    }
}
