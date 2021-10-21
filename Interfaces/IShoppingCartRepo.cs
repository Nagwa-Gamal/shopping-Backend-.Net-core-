using shopping.Entity;
using shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Interfaces
{
    public interface IShoppingCartRepo
    {
        IQueryable<ShoppingCartVM> GetAll();
        ShoppingCartVM Add(ShoppingCartVM ob);
        ShoppingCartVM Delete(int id);
        ShoppingCartVM Edit(ShoppingCartVM ob);
        ShoppingCartVM Update(ShoppingCartVM ob);
        ShoppingCartVM DeleteUsingAttr(ShoppingCartVM ob);
        ShoppingCartVM updateAndAddToOrders(ShoppingCartVM ob);
        ShoppingCartVM updateAndRemoveFromOrders(ShoppingCartVM ob);

        ShoppingCartVM GetById(int id);
        ShoppingCartVM GetQuantity(int productId,string UId);
        IQueryable<ShoppingCartVM> Search(string name);
    }
}
