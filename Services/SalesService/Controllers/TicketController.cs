using AutoMapper;
using SalesService.DTOs;
using SalesService.Models;
using SalesService.Repositories;
using SalesService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly InventoryClient _inventoryClient;

        public TicketController(ITicketRepository ticketRepository, IMapper mapper, InventoryClient inventoryClient)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _inventoryClient = inventoryClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketSystemDto>>> GetAllTickets()
        {
            var tickets = await _ticketRepository.GetAllTicketsAsync();
            return Ok(_mapper.Map<IEnumerable<TicketSystemDto>>(tickets));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketSystemDto>> GetTicket(int id)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<TicketSystemDto>(ticket));
        }

        [HttpGet("check-availability")]
        public async Task<ActionResult<bool>> IsProductAvailable([FromQuery] int productId, [FromQuery] int quantity)
        {
            var isAvailable = await _inventoryClient.CheckProductAvailability(productId, quantity);
            return Ok(isAvailable);
        }

        [HttpPost]
        public async Task<ActionResult<TicketSystemDto>> CreateTicket(TicketSystemDto ticketDto)
        {
            // Check product availability
            bool isAvailable = await _inventoryClient.CheckProductAvailability(ticketDto.ProductId, ticketDto.Quantity);

            if (!isAvailable)
            {
                return BadRequest("Requested quantity is not available in stock.");
            }

            // Proceed with ticket creation if product is available
            var ticket = _mapper.Map<TicketSystem>(ticketDto);
            await _ticketRepository.AddTicketAsync(ticket);

            return CreatedAtAction(nameof(GetTicket), new { id = ticket.TicketId }, _mapper.Map<TicketSystemDto>(ticket));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, TicketSystemDto ticketDto)
        {
            if (id != ticketDto.TicketId)
            {
                return BadRequest();
            }

            var ticket = _mapper.Map<TicketSystem>(ticketDto);
            await _ticketRepository.UpdateTicketAsync(ticket);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            await _ticketRepository.DeleteTicketAsync(id);
            return NoContent();
        }
    }
}