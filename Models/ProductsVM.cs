using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Models
{
    public class ProductsVM
    {

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Product Name Required")]

        public string Name { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Price Required")]

        public string Price { get; set; }

        [StringLength(550)]
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }

    }
}
