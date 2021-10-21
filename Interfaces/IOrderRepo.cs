using shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Interfaces
{
   public interface IOrderRepo
    {
        IQueryable<OrdersVM> GetAll();
        OrdersVM Add(OrdersVM ob);
        OrdersVM Delete(int id);
        OrdersVM Edit(OrdersVM ob);
        OrdersVM GetById(int id);
        OrdersVM updateAndRemoveFromOrders(OrdersVM ob);

        IQueryable<OrdersVM> Search(string name);
    }
}
