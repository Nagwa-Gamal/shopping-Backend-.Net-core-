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
    public class ProductRepo : IProductRepo
    {
        private readonly DBContext db;
        private readonly IMapper mapper;

        public ProductRepo(DBContext db,IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public ProductsVM Add(ProductsVM ob)
        {
            Products data = mapper.Map<Products>(ob);
            db.Products.Add(data);
            db.SaveChanges();
            return ob;
                }

        public ProductsVM Delete(int id)
        {
            try
            {
                Products d = db.Products.Where(n => n.Id == id).FirstOrDefault();
                db.Remove(d);
                db.SaveChanges();
                ProductsVM data = mapper.Map<ProductsVM>(d);
                return data;
            }
            catch
            {
                return null;
            }
        }

        public ProductsVM Edit(ProductsVM ob)
        {
            try
            {

                var data = mapper.Map<Products>(ob);
                db.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return ob;
            }
            catch
            {
                return null;
            }
        }

        public IQueryable<ProductsVM> GetAll()
        {
            var data = db.Products.Select(n => new ProductsVM { Id = n.Id, Name = n.Name, Price = n.Price, ImageUrl = n.ImageUrl });
            return data;
        }

        public List<ProductsVM> GetAllWithQuantityInShoppingCart(string userId)
        {
            List<ProductsVM>data = db.Products.Select(n => new ProductsVM { Id = n.Id, Name = n.Name, Price = n.Price, ImageUrl = n.ImageUrl }).ToList();
            for (int i = 0; i < data.Count; i++)
            {
                ShoppingCartVM s = db.ShoppingCart.Where(n => n.ProductId == data[i].Id).Where(n => n.UserId == userId).Select(n => new ShoppingCartVM {quantity = n.quantity }).FirstOrDefault();
                if (s != null)
                    data[i].Quantity = s.quantity;
                else
                    data[i].Quantity = 0;
            }
            return data;
        }

        public ProductsVM GetById(int id)
        {
            ProductsVM data = db.Products.Where(n => n.Id == id).Select(n => new ProductsVM { Id = n.Id, Name = n.Name, Price = n.Price, ImageUrl = n.ImageUrl }).FirstOrDefault();
            return data;
        }

        public IQueryable<ProductsVM> Search(string name)
        {
            var data = db.Products.Where(n => n.Name.Contains(name)).Select(n => new ProductsVM { Id = n.Id, Name = n.Name, Price = n.Price, ImageUrl = n.ImageUrl });
            return data;
        }
    }
}
