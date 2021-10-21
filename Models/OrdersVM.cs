using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Models
{
    public class OrdersVM
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }

        public int ProductId { get; set; }

        public int quantity { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string ImageUrl { get; set; }
    }
}
