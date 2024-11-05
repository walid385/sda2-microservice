using AutoMapper;
using CustomerService.DTOs;
using CustomerService.Models;
using CustomerService.Repositories;
using CustomerService.Services;
using Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;


namespace CustomerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        public CartsController(ICartRepository repository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;

        }

        // GET: api/Carts/ByCustomer/5
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(int customerId)
        {
            var carts = await _repository.GetCartsByCustomerIdAsync(customerId);

            if (carts == null || !carts.Any() || !carts.SelectMany(cart => cart.ItemLists).Any())
            {
                return BadRequest("Cart is empty.");
            }

            foreach (var item in carts.SelectMany(cart => cart.ItemLists))
            {
                var orderEvent = new OrderCreatedEvent
                {
                    OrderId = 0, // Set dynamically or leave as default if not available
                    CustomerId = customerId,
                    OrderDate = DateTime.UtcNow,
                    TotalAmount = item.UnitPrice * item.Quantity,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                // Publish the event with a routing key
                await _publishEndpoint.Publish(orderEvent, context =>
                {
                    context.SetRoutingKey("orderCreated"); // Injecting the routing key
                });
            }

            return Ok("All orders placed successfully.");
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
