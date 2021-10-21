using shopping.Authentication;
using shopping.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace shopping.Entity
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User user { get; set; }
      
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Products Product { get; set; }
        public bool complete { get; set; }
        public int quantity { get; set; }


    }
}
