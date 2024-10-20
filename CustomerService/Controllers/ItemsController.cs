using AutoMapper;
using CustomerService.DTOs;
using CustomerService.Models;
using CustomerService.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemListRepository _repository;
        private readonly IMapper _mapper;

        public ItemsController(IItemListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/ItemList/ByCart/5
        [HttpGet("ByCart/{cartId}")]
        public async Task<ActionResult<IEnumerable<ItemListDto>>> GetItemsByCart(int cartId)
        {
            var items = await _repository.GetItemsByCartIdAsync(cartId);
            var itemDtos = _mapper.Map<IEnumerable<ItemListDto>>(items);
            return Ok(itemDtos);
        }

        // GET: api/ItemList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemListDto>> GetItem(int id)
        {
            var item = await _repository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var itemDto = _mapper.Map<ItemListDto>(item);
            return Ok(itemDto);
        }

        // POST: api/ItemList
        [HttpPost]
        public async Task<ActionResult<ItemListDto>> CreateItem(ItemListDto itemDto)
        {
            var item = _mapper.Map<ItemList>(itemDto);
            await _repository.AddItemAsync(item);
            var createdItemDto = _mapper.Map<ItemListDto>(item);
            return CreatedAtAction(nameof(GetItem), new { id = createdItemDto.ItemListId }, createdItemDto);
        }

        // PUT: api/ItemList/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, ItemListDto itemDto)
        {
            if (id != itemDto.ItemListId)
            {
                return BadRequest();
            }

            var item = _mapper.Map<ItemList>(itemDto);
            await _repository.UpdateItemAsync(item);
            return NoContent();
        }

        // DELETE: api/ItemList/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _repository.DeleteItemAsync(id);
            return NoContent();
        }
    }
}
