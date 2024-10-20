using AutoMapper;
using CustomerService.DTOs;
using CustomerService.Models;
using CustomerService.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace CustomerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;

        public CartsController(ICartRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Carts/ByCustomer/5
        [HttpGet("ByCustomer/{customerId}")]
        public async Task<ActionResult<IEnumerable<CartInProgressDto>>> GetCartsByCustomer(int customerId)
        {
            var carts = await _repository.GetCartsByCustomerIdAsync(customerId);
            var cartDtos = _mapper.Map<IEnumerable<CartInProgressDto>>(carts);
            return Ok(cartDtos);
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartInProgressDto>> GetCart(int id)
        {
            var cart = await _repository.GetCartByIdAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            var cartDto = _mapper.Map<CartInProgressDto>(cart);
            return Ok(cartDto);
        }

        // POST: api/Carts
        [HttpPost]
        public async Task<ActionResult<CartInProgressDto>> CreateCart(CartInProgressDto cartDto)
        {
            var cart = _mapper.Map<CartInProgress>(cartDto);
            await _repository.AddCartAsync(cart);
            var createdCartDto = _mapper.Map<CartInProgressDto>(cart);
            return CreatedAtAction(nameof(GetCart), new { id = createdCartDto.CartId }, createdCartDto);
        }

        // PUT: api/Carts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, CartInProgressDto cartDto)
        {
            if (id != cartDto.CartId)
            {
                return BadRequest();
            }

            var cart = _mapper.Map<CartInProgress>(cartDto);
            await _repository.UpdateCartAsync(cart);
            return NoContent();
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            await _repository.DeleteCartAsync(id);
            return NoContent();
        }
    }
}
