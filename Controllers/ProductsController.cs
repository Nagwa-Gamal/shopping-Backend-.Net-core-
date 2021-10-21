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

    public class ProductsController : ControllerBase
    {
        private readonly IProductRepo ProductRepo;

        public ProductsController(IProductRepo product)
        {
            this.ProductRepo = product;
        }
        [HttpPost]
        public IActionResult Create(ProductsVM Product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var c = ProductRepo.Add(Product);
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
            var data = ProductRepo.GetAll();
            return Ok(data);
        }

        // [HttpPost("Delete")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = ProductRepo.Delete(id);
            if (data == null)
                return BadRequest(new response { Message = "Can Not Delete", Status = "Error" });
            return Ok(data);
        }
        // [HttpPost("Edit")]
        [HttpPut]
        public IActionResult Edit(ProductsVM ob)
        {
            var data = ProductRepo.Edit(ob);
            if (data == null)
                return BadRequest(new response { Message = "Can Not Edit", Status = "Error" });
            return Ok(data);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = ProductRepo.GetById(id);
            if (data == null)
                return BadRequest(new response { Message = "Not Found", Status = "Error" });
            return Ok(data);
        }
        [HttpGet("getWithQuantityInShoppingCart/{id}")]
        public IActionResult GetAllWithQuantityInShoppingCart(string id)
        {
            var data = ProductRepo.GetAllWithQuantityInShoppingCart(id);
            if (data == null)
                return Ok(new response { Message = "Not Found", Status = "Error" });
            return Ok(data);
        }

        [HttpGet("search/{name}")]
        public IActionResult search(string name)
        {
            var data = ProductRepo.Search(name);
            if (data == null)
                return BadRequest(new response { Message = "Not Found", Status = "Error" });
            return Ok(data);
        }

    }
}
