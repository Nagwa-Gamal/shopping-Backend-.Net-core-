using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using shopping.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace shopping.Authentication
{
    public class DBContext:IdentityDbContext<User>
    {

       public DBContext(DbContextOptions<DBContext>options):base(options)
        {

        }
        public DbSet<Products> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<User> user { get; set; }
        public DbSet<Orders> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

             //builder.Entity<User>().HasKey(u => u.Id);
           
        }


    }
}
