using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OrderManagementService.DTOs;
using OrderManagementService.Events;
using OrderManagementService.Models;
using OrderManagementService.Repositories;
using OrderManagementService.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderManagementService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly InventoryClient _inventoryClient;

        public OrdersController(
            IOrderRepository orderRepository, 
            IMapper mapper, 
            IPublishEndpoint publishEndpoint, 
            InventoryClient inventoryClient)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _inventoryClient = inventoryClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(_mapper.Map<OrderDto>(order));
        }

         [HttpPost]
        public async Task<ActionResult> CreateOrder(CreateOrderDto createOrderDto)
        {
            // Fetch product details from InventoryService
            var product = await _inventoryClient.GetProductById(createOrderDto.ProductId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            // Check product availability
            bool isAvailable = await _inventoryClient.CheckProductAvailability(createOrderDto.ProductId, createOrderDto.Quantity);
            if (!isAvailable)
            {
                return BadRequest("Requested quantity is not available.");
            }

            // Calculate the total amount
            var totalAmount = product.UnitPrice * createOrderDto.Quantity;

            // Map CreateOrderDto to Order entity
            var order = _mapper.Map<Order>(createOrderDto);
            order.OrderDate = DateTime.UtcNow;
            order.TotalAmount = totalAmount;

            // Save order to the database
            await _orderRepository.AddOrderAsync(order);

            // Map Order to OrderCreatedEvent for publishing
            var orderCreatedEvent = _mapper.Map<OrderCreatedEvent>(order);
            await _publishEndpoint.Publish(orderCreatedEvent);
            Console.WriteLine($"Published OrderCreatedEvent for Order ID: {order.OrderId}");


            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, _mapper.Map<OrderDto>(order));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderDto updateOrderDto)
        {
            // Retrieve the existing order
            var existingOrder = await _orderRepository.GetOrderByIdAsync(id);
            if (existingOrder == null)
            {
                return NotFound("Order not found.");
            }

            // Fetch product details from InventoryService
            var product = await _inventoryClient.GetProductById(updateOrderDto.ProductId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            // Check product availability
            bool isAvailable = await _inventoryClient.CheckProductAvailability(updateOrderDto.ProductId, updateOrderDto.Quantity);
            if (!isAvailable)
            {
                return BadRequest("Requested quantity is not available.");
            }

            // Calculate the total amount
            var totalAmount = product.UnitPrice * updateOrderDto.Quantity;

            // Update order properties
            existingOrder.ProductId = updateOrderDto.ProductId;
            existingOrder.Quantity = updateOrderDto.Quantity;
            existingOrder.TotalAmount = totalAmount;
            existingOrder.OrderDate = DateTime.UtcNow;

            // Update the order in the database
            await _orderRepository.UpdateOrderAsync(existingOrder);

            // Map updated order to OrderUpdatedEvent for publishing
            var orderUpdatedEvent = _mapper.Map<OrderUpdatedEvent>(existingOrder);
            await _publishEndpoint.Publish(orderUpdatedEvent);

            return CreatedAtAction(nameof(GetOrder), new { id = existingOrder.OrderId }, _mapper.Map<OrderDto>(existingOrder));
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderRepository.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
