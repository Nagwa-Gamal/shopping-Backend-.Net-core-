using AutoMapper;
using shopping.Entity;
using shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Mapper
{
    public class DomainProfile:Profile
    {
        public DomainProfile()
        {
            CreateMap<Products, ProductsVM>();
            CreateMap<ProductsVM, Products>();

            CreateMap<ShoppingCart, ShoppingCartVM>();
            CreateMap<ShoppingCartVM, ShoppingCart>();

            CreateMap<Orders, OrdersVM>();
            CreateMap<OrdersVM, Orders>();
        }
    }
}
