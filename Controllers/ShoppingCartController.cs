using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopping.Authentication;
using shopping.Interfaces;
using shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace shopping.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepo ShoppingCartRepo;

        private readonly UserManager<User> UserManager;

        public int Quantity { get; private set; }

        public ShoppingCartController(IShoppingCartRepo ShoppingCart,UserManager<User> userManager)
        {
            this.ShoppingCartRepo = ShoppingCart;
            UserManager = userManager;
        }
        [HttpPost]
        public IActionResult Create(ShoppingCartVM ShoppingCart)
        {
          
          
            if (ModelState.IsValid)
            {
                try
                {
                    
                    var c = ShoppingCartRepo.Add(ShoppingCart);
                    return Ok(c);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }

            var data = new response();
            var errors = ModelState.Values;
            foreach (var item in errors)
            {
                data.Errors.Add(item.Errors.ToString());
            }
            data.Message = "Invalid Data";
            data.Status = "Error";
            return BadRequest(data);
        }

        // [HttpGet("GetAll")]
        [HttpGet]
        public IActionResult Get()
        {
            var data = ShoppingCartRepo.GetAll();
            return Ok(data);
        }

        // [HttpPost("Delete")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = ShoppingCartRepo.Delete(id);
            if (data == null)
                return BadRequest(new response { Message = "Can Not Delete", Status = "Error" });
            return Ok(data);
        }
        [HttpDelete("delete")]
        public IActionResult Delete(ShoppingCartVM ob)
        {
            var data = ShoppingCartRepo.DeleteUsingAttr(ob);
            if (data == null)
                return BadRequest(new response { Message = "Can Not Delete", Status = "Error" });
            return Ok(data);
        }
        // [HttpPost("Edit")]
        [HttpPut]
        public   IActionResult Edit(ShoppingCartVM ob)
        {
            var data =   ShoppingCartRepo.Edit(ob);
            if (data == null)
                return BadRequest(new response { Message = "Can Not Edit", Status = "Error" });
            return Ok(data);
        }
        [HttpPut("update")]
        public IActionResult Update(ShoppingCartVM ob)
        {
            var data = ShoppingCartRepo.Update(ob);
            if (data == null)
                return BadRequest(new response { Message = "Can Not Edit", Status = "Error" });
            return Ok(data);
        }
        [HttpPut("updateAndAddToOrders")]
        public IActionResult updateAndAddToOrders(ShoppingCartVM ob)
        {
            var data = ShoppingCartRepo.updateAndAddToOrders(ob);
            if (data == null)
                return BadRequest(new response { Message = "Can Not Edit", Status = "Error" });
            return Ok(data);
        }
        [HttpPut("updateAndRemoveFromOrders")]
        public IActionResult updateAndRemoveFromOrders(ShoppingCartVM ob)
        {
            var data = ShoppingCartRepo.updateAndRemoveFromOrders(ob);
            if (data == null)
                return BadRequest(new response { Message = "Can Not Edit", Status = "Error" });
            return Ok(data);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = ShoppingCartRepo.GetById(id);
            if (data == null)
                return BadRequest(new response { Message = "Not Found", Status = "Error" });
            return Ok(data);
        }
        [HttpGet("getQuantity/{id}/{uId}")]
        public IActionResult GetQuantity(int id,string uId)
        {
            var data = ShoppingCartRepo.GetQuantity(id,uId);
            Quantity = 0;
            if (data==null )
                return Ok(new { Quantity});
            return Ok(data);
        }
        [HttpGet("search/{name}")]
        public IActionResult search(string name)
        {
            var data = ShoppingCartRepo.Search(name);
            if (data == null)
                return BadRequest(new response { Message = "Not Found", Status = "Error" });
            return Ok(data);
        }

    }
}
