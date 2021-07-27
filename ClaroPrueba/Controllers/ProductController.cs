using ClaroPrueba.Dtos;
using ClaroPrueba.Entities;
using ClaroPrueba.Persistence;
using ClaroPrueba.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaroPrueba.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController:ControllerBase
    {
        private readonly IProductService _productService;
        
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll() 
        {
            var dbEntities = await _productService.GetAll();
            return dbEntities;
        }
        [HttpGet("{Id}", Name="GetProduct")]
        public async Task<ActionResult<ProductDto>> Get(int? Id) 
        {
            var exist = await _productService.FindId(Id);
            if (!exist) { return BadRequest($"The productId :{Id} does not exist on database."); }

            var dbentity = await _productService.Get(Id);
            return dbentity;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductDto model)
        {
            if (ModelState.IsValid) 
            {
               await _productService.Post(model);
               return new CreatedAtRouteResult("GetProduct", new { Id = model.ProductID, model });
            }
            return BadRequest($"Product{model.ProductName} couldn't be created.");
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int Id, [FromBody] ProductUpdateDto model) 
        {
            if (ModelState.IsValid) 
            {
                await _productService.Update(Id, model);
                return NoContent();
            }
            return BadRequest($"Product{model.ProductName} couldn't be updated");
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await _productService.Delete(Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"ProductId: {Id} couldn't be deleted, {ex.Message}");
                throw;
            }
        
        }
    }
}
