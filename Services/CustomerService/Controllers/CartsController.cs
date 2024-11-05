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
        private readonly InventoryClient _inventoryClient;
        public CartsController(ICartRepository repository, IMapper mapper, IPublishEndpoint publishEndpoint, InventoryClient inventoryClient)
        {
            _repository = repository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _inventoryClient = inventoryClient;

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
                // Retrieve the price for each product from InventoryService
                var unitPrice = await _inventoryClient.GetProductPrice(item.ProductId);

                if (unitPrice == null)
                {
                    return BadRequest($"Price for product {item.ProductId} not found.");
                }

                // Update item with the retrieved unit price
                item.UnitPrice = unitPrice.Value;

                var orderEvent = new OrderCreatedEvent
                {
                    OrderId = 0, // Set dynamically or leave as default if not available
                    CustomerId = customerId,
                    OrderDate = DateTime.UtcNow,
                    TotalAmount = item.UnitPrice * item.Quantity, // Calculate total amount using the retrieved price
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

        // POST: api/Carts
        [HttpPost]
        public async Task<ActionResult<CartInProgressDto>> CreateCart(CreateCartDto cartDto)
        {
            var itemLists = new List<ItemList>();

            foreach (var item in cartDto.Items)
            {
                var unitPrice = await _inventoryClient.GetProductPrice(item.ProductId);

                // Log retrieved UnitPrice
                Console.WriteLine($"Retrieved UnitPrice for ProductId {item.ProductId}: {unitPrice}");

                if (unitPrice == null)
                {
                    return BadRequest($"Price for product {item.ProductId} not found.");
                }

                // Assign UnitPrice to item
                itemLists.Add(new ItemList
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = unitPrice.Value // Ensure unitPrice is being assigned correctly
                });
            }

            var cart = new CartInProgress
            {
                CustomerId = cartDto.CustomerId,
                CreatedAt = DateTime.UtcNow,
                ItemLists = itemLists
            };

            await _repository.AddCartAsync(cart);

            // Map the saved cart to the DTO for the response
            var createdCartDto = _mapper.Map<CartInProgressDto>(cart);
            Console.WriteLine($"Created cart with ItemLists: {string.Join(", ", cart.ItemLists.Select(i => $"ProductId: {i.ProductId}, UnitPrice: {i.UnitPrice}"))}");

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
