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
    public class OrdersRepo : IOrderRepo
    {

        private readonly DBContext db;
        private readonly IMapper mapper;

        public OrdersRepo(DBContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public OrdersVM Add(OrdersVM ob)
        {
            Orders data = mapper.Map<Orders>(ob);
            db.Orders.Add(data);
            db.SaveChanges();
            return ob;
        }

        public OrdersVM Delete(int id)
        {
            try
            {
                Orders d = db.Orders.Where(n => n.Id == id).FirstOrDefault();
                db.Remove(d);
                db.SaveChanges();
               OrdersVM data = mapper.Map<OrdersVM>(d);
                return data;
            }
            catch
            {
                return null;
            }
        }

        public OrdersVM Edit(OrdersVM ob)
        {

            try
            {
                var data = mapper.Map<Orders>(ob);
                db.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return ob;
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<OrdersVM> GetAll()
        {
            var data = db.Orders.Select(n => new OrdersVM { Id = n.Id, UserId = n.UserId,  ProductId = n.ProductId, ImageUrl = n.Product.ImageUrl, Name = n.Product.Name, Price = n.Product.Price, quantity = n.quantity });

            return data;
        }

        public OrdersVM GetById(int id)
        {
            OrdersVM data = db.Orders.Where(n => n.Id == id).Select(n => new OrdersVM { Id = n.Id, UserId = n.UserId, ProductId = n.ProductId, ImageUrl = n.Product.ImageUrl, Name = n.Product.Name, Price = n.Product.Price,quantity=n.quantity }).FirstOrDefault();
            return data;
        }

        public IQueryable<OrdersVM> Search(string name)
        {
            var data = db.Orders.Where(n => n.Product.Name.Contains(name)).Select(n => new OrdersVM { Id = n.Id, ImageUrl = n.Product.ImageUrl, Name = n.Product.Name, Price = n.Product.Price, UserId = n.UserId, ProductId = n.ProductId, quantity = n.quantity });
            return data;
        }

        public OrdersVM updateAndRemoveFromOrders(OrdersVM ob)
        {
            try
            {
                // remove product from orders
                Orders d = db.Orders.Where(n => n.Id == ob.Id).FirstOrDefault();
                db.Remove(d);
                db.SaveChanges();
                // find id of product in shopping cart
                ShoppingCartVM o = db.ShoppingCart.Where(n => n.ProductId == ob.ProductId).Where(n => n.UserId == ob.UserId).Select(n => new ShoppingCartVM { Id = n.Id, UserId = n.UserId, ProductId = n.ProductId, complete = n.complete, quantity = n.quantity }).FirstOrDefault();

                //update product in shopping cart
                o.complete = false;
                var data = mapper.Map<ShoppingCart>(o);

                db.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
