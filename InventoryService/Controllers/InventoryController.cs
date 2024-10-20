using AutoMapper;
using InventoryService.DTOs;
using InventoryService.Models;
using InventoryService.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _repository;
        private readonly IMapper _mapper;

        public InventoryController(IInventoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductInventoryDto>>> GetAllProducts()
        {
            var products = await _repository.GetAllProductsAsync();
            return Ok(_mapper.Map<IEnumerable<ProductInventoryDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInventoryDto>> GetProduct(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProductInventoryDto>(product));
        }

        [HttpPost]
        public async Task<ActionResult<ProductInventoryDto>> CreateProduct(ProductInventoryDto productDto)
        {
            var product = _mapper.Map<ProductInventory>(productDto);
            await _repository.AddProductAsync(product);
            var createdProductDto = _mapper.Map<ProductInventoryDto>(product);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProductDto.ProductId }, createdProductDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductInventoryDto productDto)
        {
            if (id != productDto.ProductId)
            {
                return BadRequest();
            }

            var product = _mapper.Map<ProductInventory>(productDto);
            await _repository.UpdateProductAsync(product);
            var updatedProductDto = _mapper.Map<ProductInventoryDto>(product);
            return Ok(new { Message = "Product updated successfully", Product = updatedProductDto });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            // Now, delete the product
            await _repository.DeleteProductAsync(id);

            // Return the deleted product details as confirmation
            var deletedProductDto = _mapper.Map<ProductInventoryDto>(product);
            return Ok(new { Message = "Product deleted successfully", Product = deletedProductDto });
        }
    }
}
