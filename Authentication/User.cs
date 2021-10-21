using Microsoft.AspNetCore.Identity;
using shopping.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Authentication
{
    public class User:IdentityUser
    {
     public virtual ICollection<ShoppingCart> ShoppingCart { get; set; }
       public  string isAdmin { get; set ; }

    }
}
