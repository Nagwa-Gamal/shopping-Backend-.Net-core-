using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shopping.Authentication;
using shopping.Interfaces;
using shopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepo OrdersRepo;

        public OrdersController(IOrderRepo Orders)
        {
            this.OrdersRepo = Orders;
        }
        [HttpPost]
        public IActionResult Create(OrdersVM Orders)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var c = OrdersRepo.Add(Orders);
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
            var data = OrdersRepo.GetAll();
            return Ok(data);
        }

        // [HttpPost("Delete")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = OrdersRepo.Delete(id);
            if (data == null)
                return BadRequest(new response { Message = "Can Not Delete", Status = "Error" });
            return Ok(data);
        }
        // [HttpPost("Edit")]
        [HttpPut]
        public IActionResult Edit(OrdersVM ob)
        {
            var data = OrdersRepo.Edit(ob);
            if (data == null)
                return BadRequest(new response { Message = "Can Not Edit", Status = "Error" });
            return Ok(data);
        }
        [HttpPut("updateAndRemoveFromOrders")]
        public IActionResult updateAndRemoveFromOrders(OrdersVM ob)
        {
            var data = OrdersRepo.updateAndRemoveFromOrders(ob);
            if (data == null)
                return BadRequest(new response { Message = "Can Not Edit", Status = "Error" });
            return Ok(data);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = OrdersRepo.GetById(id);
            if (data == null)
                return BadRequest(new response { Message = "Not Found", Status = "Error" });
            return Ok(data);
        }

        [HttpGet("search/{name}")]
        public IActionResult search(string name)
        {
            var data = OrdersRepo.Search(name);
            if (data == null)
                return BadRequest(new response { Message = "Not Found", Status = "Error" });
            return Ok(data);
        }
    }
}
