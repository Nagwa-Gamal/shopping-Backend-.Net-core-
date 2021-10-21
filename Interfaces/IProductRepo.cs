using shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Interfaces
{
   public  interface IProductRepo
    {
        IQueryable<ProductsVM> GetAll();
       List<ProductsVM> GetAllWithQuantityInShoppingCart(string userId);

        ProductsVM Add(ProductsVM ob);
        ProductsVM Delete(int id);
        ProductsVM Edit(ProductsVM ob);
        ProductsVM GetById(int id);
        IQueryable<ProductsVM> Search(string name);
    }
}
