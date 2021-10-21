using AutoMapper;
using shopping.Authentication;
using shopping.Entity;
using shopping.Interfaces;
using shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Repo
{
    public class ShoppingCartRepo : IShoppingCartRepo
    {

        private readonly DBContext db;
        private readonly IMapper mapper;

        public ShoppingCartRepo(DBContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public ShoppingCartVM Add(ShoppingCartVM ob)
        {
            ShoppingCart data = mapper.Map<ShoppingCart>(ob);
            db.ShoppingCart.Add(data);
            db.SaveChanges();
            return ob;

        }

        public ShoppingCartVM Delete(int id)
        {
            try
            {
                ShoppingCart d = db.ShoppingCart.Where(n => n.Id == id).FirstOrDefault();
                db.Remove(d);
                db.SaveChanges();
                ShoppingCartVM data = mapper.Map<ShoppingCartVM>(d);
                return data;
            }
            catch
            {
                return null;
            }
        }
        public ShoppingCartVM DeleteUsingAttr(ShoppingCartVM ob)
        {
            try
            {
                ShoppingCartVM s = db.ShoppingCart.Where(n => n.ProductId == ob.ProductId).Where(n => n.UserId == ob.UserId).Select(n => new ShoppingCartVM { Id = n.Id, UserId = n.UserId, complete = n.complete, ProductId = n.ProductId, ImageUrl = n.Product.ImageUrl, Name = n.Product.Name, Price = n.Product.Price, quantity = n.quantity }).FirstOrDefault();

       
                ShoppingCart d = db.ShoppingCart.Where(n => n.Id == s.Id).FirstOrDefault();
                db.Remove(d);
                db.SaveChanges();
                ShoppingCartVM data = mapper.Map<ShoppingCartVM>(d);
                return data;
            }
            catch
            {
                return null;
            }
        }

      
        public  ShoppingCartVM Edit(ShoppingCartVM ob)
        {
            try
            {
                var data =  mapper.Map<ShoppingCart>(ob);
                db.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return ob;
            }
            catch
            {
                return null;
            }
        }
        public ShoppingCartVM Update(ShoppingCartVM ob)

        {
            try
            {
                ShoppingCartVM s = db.ShoppingCart.Where(n => n.ProductId == ob.ProductId).Where(n => n.UserId == ob.UserId).Select(n => new ShoppingCartVM { Id = n.Id, UserId = n.UserId, complete = n.complete, ProductId = n.ProductId, ImageUrl = n.Product.ImageUrl, Name = n.Product.Name, Price = n.Product.Price, quantity = n.quantity }).FirstOrDefault();

                var data = mapper.Map<ShoppingCart>(ob);
                data.Id = s.Id;
                 db.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return ob;
            }
            catch
            {
                return null;
            }
        }
        public IQueryable<ShoppingCartVM> GetAll()
        {
            var data = db.ShoppingCart.Select(n => new ShoppingCartVM { Id = n.Id,UserId=n.UserId,complete=n.complete,ProductId=n.ProductId,ImageUrl=n.Product.ImageUrl,Name=n.Product.Name,Price=n.Product.Price,quantity=n.quantity });
           
            return data;
        }

        public ShoppingCartVM GetById(int id)
        {
            ShoppingCartVM data = db.ShoppingCart.Where(n => n.Id == id).Select(n => new ShoppingCartVM { Id = n.Id, UserId = n.UserId, complete = n.complete, ProductId = n.ProductId, ImageUrl = n.Product.ImageUrl, Name = n.Product.Name, Price = n.Product.Price, quantity = n.quantity }).FirstOrDefault();
            return data;
        }

        public ShoppingCartVM GetQuantity(int ProductId,string UId)
        {
            ShoppingCartVM data = db.ShoppingCart.Where(n => n.ProductId == ProductId).Where(n=> n.UserId == UId).Select(n => new ShoppingCartVM { Id = n.Id, UserId = n.UserId, complete = n.complete, ProductId = n.ProductId, ImageUrl = n.Product.ImageUrl, Name = n.Product.Name, Price = n.Product.Price, quantity = n.quantity }).FirstOrDefault();
            return data;
        }

        public IQueryable<ShoppingCartVM> Search(string name)
          {
              var data = db.ShoppingCart.Where(n => n.Product.Name.Contains(name)).Select(n => new ShoppingCartVM { Id = n.Id, ImageUrl = n.Product.ImageUrl, Name = n.Product.Name, Price = n.Product.Price, UserId = n.UserId, complete = n.complete, ProductId = n.ProductId, quantity = n.quantity });
              return data;
          }

        public ShoppingCartVM updateAndAddToOrders(ShoppingCartVM ob)
        {
            try
            {
                //update product in shopping cart
                var data = mapper.Map<ShoppingCart>(ob);
                db.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                // add product to orders
                OrdersVM o = new OrdersVM { ProductId = ob.ProductId, UserId = ob.UserId, quantity = ob.quantity };
                Orders d= mapper.Map<Orders>(o);
                db.Orders.Add(d);
                db.SaveChanges();
                return ob;
            }
            catch
            {
                return null;
            }

        }
       public ShoppingCartVM updateAndRemoveFromOrders(ShoppingCartVM ob)
        {
            try
            {
                //update product in shopping cart
                var data = mapper.Map<ShoppingCart>(ob);
                db.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                // find id of product in orders
                OrdersVM o = db.Orders.Where(n => n.ProductId == ob.ProductId).Where(n => n.UserId == ob.UserId).Select(n => new OrdersVM { Id = n.Id, UserId = n.UserId, ProductId = n.ProductId, ImageUrl = n.Product.ImageUrl, Name = n.Product.Name, Price = n.Product.Price, quantity = n.quantity }).FirstOrDefault();

                // remove product from orders
                Orders d = db.Orders.Where(n => n.Id == o.Id).FirstOrDefault();
                db.Remove(d);
                db.SaveChanges();
                return ob;
            }
            catch
            {
                return null;
            }
        }


    }
}
