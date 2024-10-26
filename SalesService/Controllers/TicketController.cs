using AutoMapper;
using SalesService.DTOs;
using SalesService.Models;
using SalesService.Repositories;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<bool> IsProductAvailable(int productId, int quantity)
        {
            return await _inventoryClient.CheckProductAvailability(productId, quantity);
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

        [HttpPost]
        public async Task<ActionResult<TicketSystemDto>> CreateTicket(TicketSystemDto ticketDto)
        {
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
