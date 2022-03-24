using Inventory.Product.Data;
using Inventory.Product.Dtos;
using Inventory.Product.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IItemRepository _repository;

        public ProductController(IItemRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetProduct(string Name = null)
        {
            var products = (await _repository.GetProducts()).Select(item => item.AsDto());

            if (!string.IsNullOrWhiteSpace(Name))
                products = products.Where(p => p.Name.Contains(Name, StringComparison.OrdinalIgnoreCase));

            return Ok(products);
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<ActionResult<ItemDto>> GetProductById(int Id)
        { 
            var product =await _repository.GetItemById(Id);
            if(product == null)
                return NotFound();

            return Ok(product.AsDto());
        } 

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateProduct(CreateItemDto itemDto)
        {
            Item item = new Item()
            {
                Name = itemDto.Name,
                Description = itemDto.Description
            };
            await _repository.CreateItem(item); 

            return CreatedAtAction(nameof(GetProductById), new { Id = item.Id }, item.AsDto());
        }
        [HttpPut("{productId}")]
        public async Task<ActionResult<ItemDto>> UpdateProduct(int productId,CreateItemDto itemDto)
        {
            var existingItem = await _repository.GetItemById(productId);
            if (existingItem == null)
                return NotFound();

            existingItem.Description = itemDto.Description;
            existingItem.Name = itemDto.Name;
             
           await _repository.UpdateItem(existingItem); 

            return CreatedAtAction(nameof(GetProductById), new { Id = existingItem.Id }, existingItem.AsDto());
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult<ItemDto>> DeleteProduct(int productId)
        {
            var existingItem = await _repository.GetItemById(productId);
            if (existingItem == null)
                return NotFound(); 

            await _repository.DeleteItem(productId);

            return CreatedAtAction(nameof(GetProductById), new { Id = existingItem.Id }, existingItem.AsDto());
        }
    }
}
