using shopping.Authentication;
using shopping.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Models
{
    public class ShoppingCartVM
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }

        public int ProductId { get; set; }

        public bool complete { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string ImageUrl { get; set; }
        //public virtual Products Product { get; set; }
        public int quantity { get; set; }

    }
}
